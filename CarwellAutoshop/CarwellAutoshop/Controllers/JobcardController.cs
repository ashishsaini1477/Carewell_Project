using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarwellAutoshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobcardController : ControllerBase
    {
        private readonly IJobcardService _jobcardService;
        public JobcardController(IJobcardService jobcardService)
        {
            _jobcardService = jobcardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobCardAsync(CreateJobCardDto createJobCardDto)
        {
            var result = await _jobcardService.CreateJobCardAsync(createJobCardDto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetJobcardByIdAsync(int jobCardId)
        {
            var result = await _jobcardService.GetJobcardByIdAsync(jobCardId);
            return Ok(result);
        }
        [HttpPost("AddRemark")]
        public async Task<IActionResult> AddRemarkAsync(JobCardRemarkDto jobCardRemarkDto)
        {
            await _jobcardService.AddRemarkAsync(jobCardRemarkDto);
            return Ok(true);
        }
        [HttpGet("all-jobcards")]
        public async Task<IActionResult> GetAllJobCards([FromQuery] PaginationRequest request)   
        {
            var result = await _jobcardService.GetAllJobCards(request);
            return Ok(result);
        }

    }
}
