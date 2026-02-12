using AutoMapper;
using CarwellAutoshop.Domain.DTOs.Request;
using CarwellAutoshop.Domain.Entities;
using CarwellAutoshop.Infrastructure.Interface;
using CarwellAutoshop.Infrastructure.Repositories;

namespace CarwellAutoshop.Infrastructure
{
    
    public class JobCardRemarkData : IJobCardRemarkData
    {
        private readonly IRepository<JobCardRemark> _repo;
        private readonly IMapper _mapper;

        public JobCardRemarkData(IRepository<JobCardRemark> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task AddRemarkAsync(JobCardRemarkDto jobCardRemarkDto)
        {
            var jobCard = _mapper.Map<JobCardRemark>(jobCardRemarkDto);
            await _repo.AddAsync(jobCard);
        }

    }
}
