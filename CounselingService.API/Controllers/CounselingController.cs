using CounselingService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CounselingService.API.Controllers
{
    [ApiController]
    [Route("api/CounselingServices")]
    public class CounselingController : ControllerBase
    {
        //Returns lisst of Counseling Objects, using Ok helper method.
        [HttpGet]
        public ActionResult<IEnumerable<CounselingDTO>> GetCounselingServices()
        {
            return Ok(CounselingDataStore.Current.CounselingServices);
        }

        [HttpGet("{id}")]
        public ActionResult<CounselingDTO> GetCounselingService(int id) 
        {
            //Returns city & Validates Response
            var CounselingToReturn = CounselingDataStore.Current.CounselingServices.FirstOrDefault(c => c.Id == id);
            
            //Returns Not Found, if ID does not exist.
            if (CounselingToReturn == null) 
            {
                return NotFound();
            }

            return Ok(CounselingToReturn);
        }
    }
}
