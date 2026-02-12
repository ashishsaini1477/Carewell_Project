using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarwellAutoshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleDataAsync(VehicleDto createVehicleDto)
        {
            var result = await _vehicleService.AddVehicleDataAsync(createVehicleDto);
            return Ok(result);
        }
        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetVehicleDataByCustomerId(int customerId)
        {
            var result = await _vehicleService.GetVehicleDataByCustomerId(customerId);
            return Ok(result);
        }
        [HttpPost("add-jobcard-vehicle")]
        public async Task<IActionResult> AddJobcardWithVehicleDataAsync(VehicleAndJobcardDto request)
        {
            var result = await _vehicleService.AddJobcardWithVehicleDataAsync(request);
            return Ok(result);
        }
        [HttpPost("edit-jobcard-vehicle")]
        public async Task<IActionResult> UpdateJobcardWithVehicleDataAsync(EditVehicleAndJobcardDto request)
        {
            var result = await _vehicleService.UpdateJobcardWithVehicleDataAsync(request);
            return Ok(result);
        }
    }
}
