using CounselingService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CounselingService.API.Controllers
{
    [Route("api/CounselingServices/{counselingID}/SpecialEvents")]
    [ApiController]
    public class SpecialEventsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<SpecialEventsDTO>> GetSpecialEvents(int counselingID) 
        {
            var counseling = CounselingDataStore.Current.CounselingServices.FirstOrDefault(c => c.Id == counselingID);

            if (counseling == null) 
            {
                return NotFound();
            }
            return Ok(counseling.SpecialEvents);
        }

        [HttpGet("{specialEventID}", Name = "GetSpecialEvent")]
        public ActionResult<SpecialEventsDTO> GetSpecialEvent(int counselingID, int specialEventID)
        {
            //Find Counseling
            var counseling = CounselingDataStore.Current.CounselingServices.FirstOrDefault(c => c.Id == counselingID);

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

            var counseling = CounselingDataStore.Current.CounselingServices.FirstOrDefault(c => c.Id == counselingID);
            if (counseling == null) 
            {
                return NotFound();
            }
            //demo - Will refactor later
            var maxSpecialEventsId = CounselingDataStore.Current.CounselingServices.SelectMany(c => c.SpecialEvents).Max(s => s.Id);

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
    }
}
