using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Infrastructure.Interface
{
    public interface IJobcardData
    {
        Task<int> CreateJobCardAsync(CreateJobCardDto dto);
        Task<JobCardResponse> GetJobcardByIdAsync(int jobCardId);
        Task<PagedResponse<JobCardResponse>> GetAllJobCards(PaginationRequest request);
        Task<JobCardResponse> UpdateJobCard(EditJobCardDto request);
    }
}
