using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface ISaccoRepository
    {
        Task<IEnumerable<SACCO>> GetAllAsync();
        Task<SACCO?> GetByIdAsync(int id);
        Task<int> CreateAsync(SACCO sacco);
        Task UpdateAsync(SACCO sacco);
        Task DeleteAsync(int id);
    }
}
