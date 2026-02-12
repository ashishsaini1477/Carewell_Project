using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Repositories;
using CarwellAutoshop.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarwellAutoshop.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerRequest customerRequest)
        {
            var result = await _customerService.CreateCustomer(customerRequest);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _customerService.GetCustomerById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerRequest request)
        {
            var response = await _customerService.UpdateCustomer(request);
            return Ok(response);
        }

        [HttpGet("{id}/vehicles")]
        public async Task<IActionResult> GetWithVehicles(int id)
        {
            return Ok(await _customerService.GetCustomerWithVehicles(id));
        }

        [HttpGet("allcustomer")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] PaginationRequest request)
        {
            return Ok(await _customerService.GetAllCustomers(request));
        }

    }
}
