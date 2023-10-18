using AutoMapper;
using CounselingService.API.Entities;
using CounselingService.API.Models;
using CounselingService.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CounselingService.API.Controllers
{
    [Route("api/CounselingServices/{counselingID}/SpecialEvents")]
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

        //    //Full update
        //    [HttpPut("{specialEventID}")]
        //    public ActionResult UpdateSpecialEvent(int counselingId, int specialEventID, SpecialEventForUpdateDto specialEvent) 
        //    {
        //        //Look for Counseling to update
        //        var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingId);
        //        if (counseling == null) 
        //        { 
        //            return NotFound(); 
        //        }

        //        //Look for special event to update
        //        var specialEventFromStore = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
        //        if (specialEventFromStore == null)
        //        {
        //            return NotFound();
        //        }
        //        specialEventFromStore.Name = specialEvent.Name;
        //        specialEventFromStore.Description = specialEvent.Description;

        //        return NoContent();
        //    }

        //    //Partial Update
        //    [HttpPatch("{specialEventID}")]
        //    public ActionResult PartiallyUpdateSpecialEvent(int counselingId, int specialEventID, JsonPatchDocument<SpecialEventForUpdateDto> patchDocument)
        //    {
        //        //Look for Counseling to update
        //        var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingId);
        //        if (counseling == null)
        //        {
        //            return NotFound();
        //        }

        //        //Look for special event to update
        //        var specialEventFromStore = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
        //        if (specialEventFromStore == null)
        //        {
        //            return NotFound();
        //        }

        //        var specialEventToPatch =
        //            new SpecialEventForUpdateDto()
        //            {
        //                Name = specialEventFromStore.Name,
        //                Description = specialEventFromStore.Description
        //            };

        //        patchDocument.ApplyTo(specialEventToPatch, ModelState);

        //        if (!ModelState.IsValid) 
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (!TryValidateModel(specialEventToPatch))
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        specialEventFromStore.Name = specialEventToPatch.Name;
        //        specialEventFromStore.Description = specialEventToPatch.Description;

        //        return NoContent();
        //    }

        //    //Deletion of Resources
        //    [HttpDelete("{specialEventID}")]
        //    public ActionResult DeleteSpecialEvent(int counselingId, int specialEventID)
        //    {
        //        //Look for Counseling to update
        //        var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingId);
        //        if (counseling == null)
        //        {
        //            return NotFound();
        //        }

        //        //Look for special event to update
        //        var specialEventFromStore = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
        //        if (specialEventFromStore == null)
        //        {
        //            return NotFound();
        //        }

        //        counseling.SpecialEvents.Remove(specialEventFromStore);
        //        _mailService.Send("Special Event canceled.", $"Special Event {specialEventFromStore.Name} with id {specialEventFromStore.Id} was deleted.");
        //        return NoContent();
        //    }
    }
}
