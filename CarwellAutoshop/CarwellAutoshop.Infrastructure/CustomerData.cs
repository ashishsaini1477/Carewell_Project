using AutoMapper;
using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Repositories;

namespace CarwellAutoshop.Infrastructure
{
    public class CustomerData : ICustomerData
    {
        private readonly IRepository<Customer> _repo;
        private readonly IMapper _mapper;

        public CustomerData(IRepository<Customer> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest)
        {
            var customer = _mapper.Map<Customer>(customerRequest);
            var result = await _repo.AddAsync(customer);
            var customerRes = _mapper.Map<CustomerResponse>(result);
            return customerRes;
        }

        public async Task<CustomerResponse> GetCustomerById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            var customerRes = _mapper.Map<CustomerResponse>(result);
            return customerRes;
        }

        public async Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
        {
            var customer = await _repo.GetByIdAsync(request.CustomerId);
            _mapper.Map(request, customer);
            await _repo.UpdateAsync(customer);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerWithVehiclesResponse> GetCustomerWithVehicles(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            var customerRes = _mapper.Map<CustomerWithVehiclesResponse>(result);
            return customerRes;
        }

        public async Task<PagedResponse<CustomerResponse>> GetAllCustomers(PaginationRequest request)
        {
            try
            {
                // 1️⃣ Base query
                var query = (await _repo.GetAllAsync())
                                .Where(c => c.IsActive);

                // 2️⃣ Total records (AFTER filter)
                var totalRecords = query.Count();

                // 3️⃣ Sorting (ALWAYS before pagination)
                query = query.OrderByDescending(c => c.CreatedDate);

                // 4️⃣ Pagination
                if (request.PageNumber > 0 && request.PageSize > 0)
                {
                    query = query
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize);
                }

                // 5️⃣ Map result
                var data = _mapper.Map<List<CustomerResponse>>(query.ToList());

                return new PagedResponse<CustomerResponse>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalRecords = totalRecords,
                    Data = data
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

       

    }
}
