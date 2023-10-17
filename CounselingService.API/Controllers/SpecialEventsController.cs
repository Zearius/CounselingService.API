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
        private readonly CounselingDataStore _counselingDataStore;

        public SpecialEventsController(ILogger<SpecialEventsController> logger, IMailService mailService, CounselingDataStore counselingDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(_mailService));
            _counselingDataStore = counselingDataStore ?? throw new ArgumentNullException(nameof(_counselingDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<SpecialEventsDTO>> GetSpecialEvents(int counselingID) 
        {
            try
            {
                var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingID);

                if (counseling == null)
                {
                    _logger.LogInformation($"Counseling Serivce with id {counselingID} wasn't found with accessing services.");
                    return NotFound();
                }
                return Ok(counseling.SpecialEvents);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for counseling serivces with {counselingID}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{specialEventID}", Name = "GetSpecialEvent")]
        public ActionResult<SpecialEventsDTO> GetSpecialEvent(int counselingID, int specialEventID)
        {
            //Find Counseling
            var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingID);

            if (counseling == null)
            {
                return NotFound();
            }

            //Validate Special Event
            var specialEvent = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
            if(specialEvent == null) 
            {
                return NotFound();
            }
            
            return Ok(specialEvent);

        }

        [HttpPost]
        public ActionResult<SpecialEventsDTO> CreateSpecialEvent(int counselingID, SpecialEventsForCreationDto specialEventID)
        {

            var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingID);
            if (counseling == null) 
            {
                return NotFound();
            }
            //demo - Will refactor later
            var maxSpecialEventsId = _counselingDataStore.CounselingServices.SelectMany(c => c.SpecialEvents).Max(s => s.Id);

            var finalSpecialEvent = new SpecialEventsDTO()
            {
                Id = ++maxSpecialEventsId,
                Name = specialEventID.Name,
                Description = specialEventID.Description
            };

            counseling.SpecialEvents.Add(finalSpecialEvent);

            return CreatedAtRoute("GetSpecialEvent",
                new
                {
                    counselingId = counselingID,
                    specialEventID = finalSpecialEvent.Id
                },
                finalSpecialEvent
                );
        }

        //Full update
        [HttpPut("{specialEventID}")]
        public ActionResult UpdateSpecialEvent(int counselingId, int specialEventID, SpecialEventForUpdateDto specialEvent) 
        {
            //Look for Counseling to update
            var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingId);
            if (counseling == null) 
            { 
                return NotFound(); 
            }

            //Look for special event to update
            var specialEventFromStore = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
            if (specialEventFromStore == null)
            {
                return NotFound();
            }
            specialEventFromStore.Name = specialEvent.Name;
            specialEventFromStore.Description = specialEvent.Description;

            return NoContent();
        }

        //Partial Update
        [HttpPatch("{specialEventID}")]
        public ActionResult PartiallyUpdateSpecialEvent(int counselingId, int specialEventID, JsonPatchDocument<SpecialEventForUpdateDto> patchDocument)
        {
            //Look for Counseling to update
            var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingId);
            if (counseling == null)
            {
                return NotFound();
            }

            //Look for special event to update
            var specialEventFromStore = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
            if (specialEventFromStore == null)
            {
                return NotFound();
            }

            var specialEventToPatch =
                new SpecialEventForUpdateDto()
                {
                    Name = specialEventFromStore.Name,
                    Description = specialEventFromStore.Description
                };

            patchDocument.ApplyTo(specialEventToPatch, ModelState);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(specialEventToPatch))
            {
                return BadRequest(ModelState);
            }

            specialEventFromStore.Name = specialEventToPatch.Name;
            specialEventFromStore.Description = specialEventToPatch.Description;

            return NoContent();
        }

        //Deletion of Resources
        [HttpDelete("{specialEventID}")]
        public ActionResult DeleteSpecialEvent(int counselingId, int specialEventID)
        {
            //Look for Counseling to update
            var counseling = _counselingDataStore.CounselingServices.FirstOrDefault(c => c.Id == counselingId);
            if (counseling == null)
            {
                return NotFound();
            }

            //Look for special event to update
            var specialEventFromStore = counseling.SpecialEvents.FirstOrDefault(c => c.Id == specialEventID);
            if (specialEventFromStore == null)
            {
                return NotFound();
            }

            counseling.SpecialEvents.Remove(specialEventFromStore);
            _mailService.Send("Special Event canceled.", $"Special Event {specialEventFromStore.Name} with id {specialEventFromStore.Id} was deleted.");
            return NoContent();
        }
    }
}
