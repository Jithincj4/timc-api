using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IFacilitatorRepository
    {
        Task<IEnumerable<Facilitator>> GetAllAsync();
        Task<Facilitator?> GetByIdAsync(int id);
        Task<int> CreateAsync(Facilitator facilitator);
        Task UpdateAsync(Facilitator facilitator);
        Task DeleteAsync(int id);
    }
}
