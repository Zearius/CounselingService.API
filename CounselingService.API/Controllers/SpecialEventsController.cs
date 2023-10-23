using Asp.Versioning;
using AutoMapper;
using CounselingService.API.Entities;
using CounselingService.API.Models;
using CounselingService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CounselingService.API.Controllers
{
    [Route("api/v{version:apiVersion}/CounselingServices/{counselingId}/SpecialEvents")]
    [Authorize(Policy = "MustBeAGamblingCounselor")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    public class SpecialEventsController : ControllerBase
    {
        private readonly ILogger<SpecialEventsController> _logger;
        private readonly IMailService _mailService;
        private readonly ICounselingInfoRepository _counselingInfoRepository;
        private readonly IMapper _mapper;

        public SpecialEventsController(ILogger<SpecialEventsController> logger, IMailService mailService, ICounselingInfoRepository counselingInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(_mailService));
            _counselingInfoRepository = counselingInfoRepository ?? throw new ArgumentNullException(nameof(_counselingInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<SpecialEventsDTO>>> GetSpecialEvents(int counselingId) 
        {
            var counselingName = User.Claims.FirstOrDefault(c => c.Type == "counseling")?.Value;

            if (!await _counselingInfoRepository.CounselingNameMatchesCounselingId(counselingName, counselingId))
            {
                return Forbid();
            };

            if (!await _counselingInfoRepository.CounselingExistsAsync(counselingId)) 
            {
                _logger.LogInformation($"Counseling with id {counselingId} was not found.");
                return NotFound();
            }

           var specialEventsForCounseling = await _counselingInfoRepository.GetSpecialEventsForCounselingAsync(counselingId);
           return Ok(_mapper.Map<IEnumerable<SpecialEventsDTO>>(specialEventsForCounseling));
        }

        [HttpGet("{specialEventID}", Name = "GetSpecialEvent")]
        public async Task<ActionResult<SpecialEventsDTO>> GetSpecialEvent(int counselingId, int specialEventId)
        {
            if (!await _counselingInfoRepository.CounselingExistsAsync(counselingId))
            {
                _logger.LogInformation($"Counseling with id {counselingId} was not found.");
                return NotFound();
            }
            var specialEventsForCounseling = await _counselingInfoRepository.GetSpecialEventsForCounselingAsync(counselingId, specialEventId);

            if (specialEventsForCounseling == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SpecialEventsDTO>(specialEventsForCounseling));

        }

        [HttpPost]
        public async Task<ActionResult<SpecialEventsDTO>> CreateSpecialEvent(int counselingId, SpecialEventsForCreationDto specialEvents)
        {

            if (!await _counselingInfoRepository.CounselingExistsAsync(counselingId))
            {
                return NotFound();
            }

            var finalSpecialEvent = _mapper.Map<Entities.SpecialEvents>(specialEvents);

            await _counselingInfoRepository.AddSpecialEventForCounselingAsync(counselingId, finalSpecialEvent);

            await _counselingInfoRepository.SaveChangesAsync();

            var createdSpecialEventToReturn = _mapper.Map<Models.SpecialEventsDTO>(finalSpecialEvent);

            return CreatedAtRoute("GetSpecialEvent",
                new
                {
                    counselingId = counselingId,
                    specialEventId = createdSpecialEventToReturn.Id
                },
                createdSpecialEventToReturn);
        }

        //Full update
        [HttpPut("{specialEventID}")]
        public async  Task<ActionResult> UpdateSpecialEvent(int counselingId, int specialEventID, SpecialEventForUpdateDto specialEvent)
        {
            //Look for Counseling to update
            if (!await _counselingInfoRepository.CounselingExistsAsync(counselingId))
            {
                return NotFound();
            }

            //Search DB for Special Event
            var specialEventsEntity = await _counselingInfoRepository.GetSpecialEventsForCounselingAsync(counselingId, specialEventID);
            if (specialEventsEntity == null) 
            {
                return NotFound();
            }

            _mapper.Map(specialEvent, specialEventsEntity);

            await _counselingInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        //Partial Update
        [HttpPatch("{specialEventID}")]
        public async Task<ActionResult> PartiallyUpdateSpecialEvent(int counselingId, int specialEventID, JsonPatchDocument<SpecialEventForUpdateDto> patchDocument)
        {
            //Look for Counseling to update
            if (!await _counselingInfoRepository.CounselingExistsAsync(counselingId))
            {
                return NotFound();
            }

            //Search DB for Special Event
            var specialEventsEntity = await _counselingInfoRepository.GetSpecialEventsForCounselingAsync(counselingId, specialEventID);
            if (specialEventsEntity == null)
            {
                return NotFound();
            }


            var specialEventToPatch = _mapper.Map<SpecialEventForUpdateDto>(specialEventsEntity);

            patchDocument.ApplyTo(specialEventToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(specialEventToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(specialEventToPatch, specialEventsEntity);

            await _counselingInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        //Deletion of Resources
        [HttpDelete("{specialEventID}")]
        public async Task<ActionResult> DeleteSpecialEvent(int counselingId, int specialEventID)
        {
            //Look for Counseling to update
            if (!await _counselingInfoRepository.CounselingExistsAsync(counselingId))
            {
                return NotFound();
            }

            //Search DB for Special Event
            var specialEventsEntity = await _counselingInfoRepository.GetSpecialEventsForCounselingAsync(counselingId, specialEventID);
            if (specialEventsEntity == null)
            {
                return NotFound();
            }

            _counselingInfoRepository.DeleteSpecialEvents(specialEventsEntity);

            await _counselingInfoRepository.SaveChangesAsync();

            _mailService.Send("Special Event canceled.", $"Special Event {specialEventsEntity.Name} with id {specialEventsEntity.Id} was deleted.");
            return NoContent();
        }
    }
}
