using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Service.Interface
{
    public interface IJobcardService
    {
        Task<int> CreateJobCardAsync(CreateJobCardDto dto);
        Task AddRemarkAsync(JobCardRemarkDto jobCardRemarkDto);
        Task<JobCardResponse> GetJobcardByIdAsync(int jobCardId);
        Task<PagedResponse<JobCardResponse>> GetAllJobCards(PaginationRequest request);

    }
}
