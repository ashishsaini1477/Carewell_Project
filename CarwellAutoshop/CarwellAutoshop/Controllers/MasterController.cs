using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Service;
using CarwellAutoshop.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarwellAutoshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("fuel-types")]
        public async Task<IActionResult> GetFuelTypeAsync()
        {
            var ras = await _masterService.GetFuelTypeAsync();
            return Ok(ras);
        }
        [HttpGet("payment-modes")]
        public async Task<IActionResult> GetPaymentModeAsync()
        {
            var ras = await _masterService.GetPaymentModeAsync();
            return Ok(ras);
        }
        [HttpGet("jobcard-status")]
        public async Task<IActionResult> GetJobCardStatusAsync()
        {
            var ras = await _masterService.GetJobCardStatusAsync();
            return Ok(ras);
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetDashboardCount()
        {
            var ras = await _masterService.GetDashboardCount();
            return Ok(ras);
        }

        [HttpGet("getcustomer-vehicle/{registrationNo}")]
        public async Task<IActionResult> GetCustomerByRegistrationNo(string registrationNo)
        {
            var ras = await _masterService.GetCustomerByRegistrationNo(registrationNo);
            return Ok(ras);
        }

        [HttpPut("update-invoice")]
        public async Task<IActionResult> UpdateInvoice(UpdateInvoiceRequestDto updateInvoiceRequestDto)
        {
            await _masterService.UpdateInvoice(updateInvoiceRequestDto);
            return Ok(new { message = "Invoice updated successfully" });
        }

    }
}
