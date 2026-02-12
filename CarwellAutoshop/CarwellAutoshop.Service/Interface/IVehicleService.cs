using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Service.Interface
{
    public interface IVehicleService
    {
        Task<VehicleResponse> AddVehicleDataAsync(VehicleDto createVehicleDto);
        Task<VehicleResponse> GetVehicleDataByCustomerId(int customerId);
        Task<VehicleResponse> AddJobcardWithVehicleDataAsync(VehicleAndJobcardDto request);
        Task<bool> UpdateJobcardWithVehicleDataAsync(EditVehicleAndJobcardDto request);
    }
}
