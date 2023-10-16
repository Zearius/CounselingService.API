using CounselingService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CounselingService.API.Controllers
{
    [ApiController]
    [Route("api/CounselingServices")]
    public class CounselingController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCounselingServices()
        {
            return new JsonResult(CounselingDataStore.Current.CounselingServices);
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
