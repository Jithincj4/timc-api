using TimcApi.Application.DTOs;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IMilestoneService
    {
        Task<IEnumerable<MilestoneDto>> GetByPatientIdAsync(int patientId);
        Task<int> CreateAsync(CreateMilestoneDto dto);
        Task UpdateAsync(UpdateMilestoneDto dto);
    }
}
