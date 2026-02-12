using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Infrastructure.Interface
{
    public interface ICustomerData
    {
        Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest);
        Task<CustomerResponse> GetCustomerById(int id);
        Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request);
        Task<CustomerWithVehiclesResponse> GetCustomerWithVehicles(int id);
        Task<PagedResponse<CustomerResponse>> GetAllCustomers(PaginationRequest request);
    }
}
