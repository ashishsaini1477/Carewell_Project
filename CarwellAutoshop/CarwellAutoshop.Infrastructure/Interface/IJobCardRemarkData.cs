using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Infrastructure.Interface
{
    public interface IJobCardRemarkData
    {
        Task AddRemarkAsync(JobCardRemarkDto jobCardRemarkDto);
    }
}
