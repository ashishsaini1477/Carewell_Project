using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;

namespace CarwellAutoshop.Infrastructure.Interface
{
    public interface IVehicleData
    {
        Task<VehicleResponse> AddVehicleDataAsync(VehicleDto createVehicleDto);
        Task<VehicleResponse> GetVehicleDataByCustomerId(int customerId);
        Task<VehicleResponse> UpdateVehicle(EditVehicleDto request);
    }
}
