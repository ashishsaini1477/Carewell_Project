using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Service.Interface;

namespace CarwellAutoshop.Service
{
    public class JobcardService : IJobcardService
    {
        private readonly IJobcardData _jobcardData;
        private readonly IJobCardRemarkData _jobCardRemarkData;
        public JobcardService(IJobcardData jobcardData, IJobCardRemarkData jobCardRemarkData)
        {
            _jobcardData = jobcardData;
            _jobCardRemarkData = jobCardRemarkData;
        }

        public async Task<int> CreateJobCardAsync(CreateJobCardDto dto)
        {
            return await _jobcardData.CreateJobCardAsync(dto);
        }
        public async Task<JobCardResponse> GetJobcardByIdAsync(int jobCardId)
        {
            return await _jobcardData.GetJobcardByIdAsync(jobCardId);
        }
        public async Task AddRemarkAsync(JobCardRemarkDto jobCardRemarkDto)
        {
            await _jobCardRemarkData.AddRemarkAsync(jobCardRemarkDto);
        }
       
        public async Task<PagedResponse<JobCardResponse>> GetAllJobCards(PaginationRequest request)
        {
          return  await _jobcardData.GetAllJobCards(request);

        }
    }
}
