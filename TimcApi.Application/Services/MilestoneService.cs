using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class MilestoneService : IMilestoneService
    {
        private readonly IMilestoneRepository _repo;
        private readonly IMapper _mapper;

        public MilestoneService(IMilestoneRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MilestoneDto>> GetByPatientIdAsync(int patientId)
        {
            var result = await _repo.GetByPatientIdAsync(patientId);
            return _mapper.Map<IEnumerable<MilestoneDto>>(result);
        }

        public async Task<int> CreateAsync(CreateMilestoneDto dto)
        {
            var milestone = new Milestone
            {
                PatientId = dto.PatientId,
                Stage = dto.Stage,
                IsCompleted = false,
                CompletedOn = null,
                Remarks = dto.Remarks
            };

            return await _repo.CreateAsync(milestone);
        }

        public async Task UpdateAsync(UpdateMilestoneDto dto)
        {
            var updated = new Milestone
            {
                MilestoneId = dto.MilestoneId,
                IsCompleted = dto.IsCompleted,
                CompletedOn = dto.IsCompleted ? DateTime.UtcNow : null,
                Remarks = dto.Remarks
            };

            await _repo.UpdateAsync(updated);
        }
    }

}
