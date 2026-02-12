using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Service.Interface;

namespace CarwellAutoshop.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerData _customerData;
        public CustomerService(ICustomerData customerData)
        {
            _customerData = customerData;
        }
        public async Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest)
        {
          return await _customerData.CreateCustomer(customerRequest);
        }

        public async Task<CustomerResponse> GetCustomerById(int id)
        {
            return await _customerData.GetCustomerById(id);
        }


        public async Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
        {
            return await _customerData.UpdateCustomer(request);
        }

        public async Task<CustomerWithVehiclesResponse> GetCustomerWithVehicles(int id)
        {
            return await _customerData.GetCustomerWithVehicles(id);
        }
        public async Task<PagedResponse<CustomerResponse>> GetAllCustomers(PaginationRequest request)
        {
            return await _customerData.GetAllCustomers(request);
        }

        
    }
}
