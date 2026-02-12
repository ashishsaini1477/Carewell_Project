using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Enums;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Service.Interface;

namespace CarwellAutoshop.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleData _vehicleData;
        private readonly IJobcardData _jobcardData;

        public VehicleService(IVehicleData vehicleData, IJobcardData jobcardData)
        {
            _vehicleData = vehicleData;
            _jobcardData = jobcardData;
        }

        public async Task<VehicleResponse> AddVehicleDataAsync(VehicleDto createVehicleDto)
        {
            return await _vehicleData.AddVehicleDataAsync(createVehicleDto);
        }

        public async Task<VehicleResponse> GetVehicleDataByCustomerId(int customerId)
        {
            return await _vehicleData.GetVehicleDataByCustomerId(customerId);
        }
        public async Task<VehicleResponse> AddJobcardWithVehicleDataAsync(VehicleAndJobcardDto request)
        {
            var vehicleDto = new VehicleDto
            {
                CustomerId = request.CustomerId,
                RegistrationNo = request.RegistrationNo,
                Model = request.Model,
                FuelTypeId = request.FuelTypeId
            };
            var vehicleResult = await _vehicleData.AddVehicleDataAsync(vehicleDto);
            if (vehicleResult != null)
            {
                var jobcardDto = new CreateJobCardDto
                {
                    VehicleId = vehicleResult.VehicleId,
                    GarageId = (int)GarageEnum.Carwell,
                    JobCardStatusId = request.JobCardStatusId,
                    OdometerReading = request.OdometerReading

                };
                await _jobcardData.CreateJobCardAsync(jobcardDto);
            }
            return vehicleResult;
        }
        public async Task<bool> UpdateJobcardWithVehicleDataAsync(EditVehicleAndJobcardDto request)
        {
            var vehicleDto = new EditVehicleDto
            {
                VehicleId = request.VehicleId.Value,
                RegistrationNo = request.RegistrationNo,
                Model = request.Model,
                FuelTypeId = request.FuelTypeId
            };
            var vehicleResult = await _vehicleData.UpdateVehicle(vehicleDto);
            if (vehicleResult != null)
            {
                var jobcardDto = new EditJobCardDto
                {
                    JobCardId = request.JobCardId,
                    JobCardStatusId = request.JobCardStatusId,
                    OdometerReading = request.OdometerReading

                };
                await _jobcardData.UpdateJobCard(jobcardDto);
            }
            return true;
        }
    }
}
