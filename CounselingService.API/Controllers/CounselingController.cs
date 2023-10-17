using CounselingService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CounselingService.API.Controllers
{
    [ApiController]
    [Route("api/CounselingServices")]
    public class CounselingController : ControllerBase
    {
        private readonly CounselingDataStore _counselingDatastore;
        public CounselingController(CounselingDataStore counselingDataStore) 
        {
            _counselingDatastore = counselingDataStore ?? throw new ArgumentNullException(nameof(counselingDataStore));

        }
        //Returns lisst of Counseling Objects, using Ok helper method.
        [HttpGet]
        public ActionResult<IEnumerable<CounselingDTO>> GetCounselingServices()
        {
            return Ok(_counselingDatastore.CounselingServices);
        }

        [HttpGet("{id}")]
        public ActionResult<CounselingDTO> GetCounselingService(int id) 
        {
            //Returns city & Validates Response
            var CounselingToReturn = _counselingDatastore.CounselingServices.FirstOrDefault(c => c.Id == id);
            
            //Returns Not Found, if ID does not exist.
            if (CounselingToReturn == null) 
            {
                return NotFound();
            }

            return Ok(CounselingToReturn);
        }
    }
}
