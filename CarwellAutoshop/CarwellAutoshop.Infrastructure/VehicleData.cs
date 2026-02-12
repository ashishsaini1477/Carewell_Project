using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.DTOs.Response;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Repositories;
using AutoMapper;
using CarwellAutoshop.Infrastructure.Data;

namespace CarwellAutoshop.Infrastructure
{
    public class VehicleData : IVehicleData
    {
        private readonly IRepository<Vehicle> _repo;
        private readonly IMapper _mapper;
        private readonly GarageDbContext _dbContext;

        public VehicleData(IRepository<Vehicle> repo, IMapper mapper, GarageDbContext dbContext)
        {
            _repo = repo;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<VehicleResponse> AddVehicleDataAsync(VehicleDto createVehicleDto)
        {
            var result = _dbContext.Vehicle.FirstOrDefault(c => c.RegistrationNo == createVehicleDto.RegistrationNo);
            if (result != null)
                return _mapper.Map<VehicleResponse>(result);
            createVehicleDto.RegistrationNo = createVehicleDto.RegistrationNo.ToUpper();
            var vehicle = _mapper.Map<Vehicle>(createVehicleDto);
            result = await _repo.AddAsync(vehicle);
            var vehicleRes = _mapper.Map<VehicleResponse>(result);
            return vehicleRes;
        }

        public async Task<VehicleResponse> GetVehicleDataByCustomerId(int customerId)
        {
            var result = await _repo.GetByIdAsync(customerId);
            var res = _mapper.Map<VehicleResponse>(result);
            return res;
        }

        public async Task<VehicleResponse> UpdateVehicle(EditVehicleDto request)
        {
            var vehicleRes = await _repo.GetByIdAsync(request.VehicleId);

            vehicleRes.RegistrationNo = request.RegistrationNo;
            vehicleRes.Model = request.Model;
            vehicleRes.FuelTypeId = request.FuelTypeId;

            await _repo.UpdateAsync(vehicleRes);
            return _mapper.Map<VehicleResponse>(vehicleRes);
        }
    }
}
