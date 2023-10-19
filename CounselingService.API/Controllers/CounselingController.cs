using Asp.Versioning;
using AutoMapper;
using CounselingService.API.Models;
using CounselingService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CounselingService.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/CounselingServices")]
    public class CounselingController : ControllerBase
    {
        private readonly ICounselingInfoRepository _counselingInfoRepository;
        private readonly IMapper _mapper;
        const int maxCounselingPageSize = 20;

        public CounselingController(ICounselingInfoRepository counselingInfoRepository,IMapper mapper) 
        {
            _counselingInfoRepository = counselingInfoRepository ?? throw new ArgumentException(nameof(counselingInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        /// <summary>
        /// Returns all Counseling Services.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="searchQuery"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>Action Result</returns>
        /// <response code="200">Returns requested Counseling Service</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CounselingWithoutSpecialEventsDTO>>> GetCounselingServices(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCounselingPageSize)
            {
                pageSize = maxCounselingPageSize;
            }

            var (counselingEntites, paginationMetadata) = await _counselingInfoRepository.GetCounselingsAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CounselingWithoutSpecialEventsDTO>>(counselingEntites));
        }


        /// <summary>
        /// Retrieves a Counseling Service by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeSpecialEvents">Wether or not to include a Special Event associated with counseling.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns requested Counseling Service</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
