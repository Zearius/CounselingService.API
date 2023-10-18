using AutoMapper;
using CounselingService.API.Models;
using CounselingService.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CounselingService.API.Controllers
{
    [ApiController]
    [Route("api/CounselingServices")]
    public class CounselingController : ControllerBase
    {
        private readonly ICounselingInfoRepository _counselingInfoRepository;
        private readonly IMapper _mapper;

        public CounselingController(ICounselingInfoRepository counselingInfoRepository,IMapper mapper) 
        {
            _counselingInfoRepository = counselingInfoRepository ?? throw new ArgumentException(nameof(counselingInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        //Returns lisst of Counseling Objects, using Ok helper method.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounselingWithoutSpecialEventsDTO>>> GetCounselingServices()
        {
            var counselingEntites = await _counselingInfoRepository.GetCounselingsAsync();


            return Ok(_mapper.Map<IEnumerable<CounselingWithoutSpecialEventsDTO>>(counselingEntites));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCounselingService(int id, bool includeSpecialEvents = false)
        {
            var counseling = await _counselingInfoRepository.GetCounselingAsync(id, includeSpecialEvents);
            if (counseling == null) 
            {
                return NotFound();
            }
            if (includeSpecialEvents)
            {
                return Ok(_mapper.Map<CounselingDTO>(counseling));
            }
            return Ok(_mapper.Map<CounselingWithoutSpecialEventsDTO>(counseling));

        }

    }
}
