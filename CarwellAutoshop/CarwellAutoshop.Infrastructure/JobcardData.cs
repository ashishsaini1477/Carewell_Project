using AutoMapper;
using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarwellAutoshop.Infrastructure
{
    public class JobcardData : IJobcardData
    {
        private readonly IRepository<JobCard> _repo;
        private readonly IMapper _mapper;

        public JobcardData(IRepository<JobCard> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }



        public async Task<int> CreateJobCardAsync(CreateJobCardDto dto)
        {
            var jobCard = _mapper.Map<JobCard>(dto);
            await _repo.AddAsync(jobCard);
            return jobCard.JobCardId;
        }

        public async Task<JobCardResponse> GetJobcardByIdAsync(int jobCardId)
        {
            var result = await _repo.GetByIdAsync(jobCardId);
            var res = _mapper.Map<JobCardResponse>(result);
            return res;
        }

        public async Task<PagedResponse<JobCardResponse>> GetAllJobCards(PaginationRequest request)
        {
            try
            {
                var query = (await _repo.GetAllAsync(
                                                    j => j.Vehicle,
                                                    j => j.Garage,
                                                    j => j.JobCardStatus
                                                    ));

                var totalRecords = query.Count();

                // 3️⃣ Sorting (ALWAYS before pagination)
                query = query.OrderByDescending(c => c.OpenDate).ToList();

                // 4️⃣ Pagination
                if (request.PageNumber > 0 && request.PageSize > 0)
                {
                    query = query
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize).ToList();
                }
                var data = _mapper.Map<List<JobCardResponse>>(query);
                return new PagedResponse<JobCardResponse>
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
        public async Task<JobCardResponse> UpdateJobCard(EditJobCardDto request)
        {
            var jobcardRes = await _repo.GetByIdAsync(request.JobCardId);

            jobcardRes.JobCardStatusId = request.JobCardStatusId;
            jobcardRes.OdometerReading = request.OdometerReading;

            await _repo.UpdateAsync(jobcardRes);
            return _mapper.Map<JobCardResponse>(jobcardRes);
        }
    }
}
