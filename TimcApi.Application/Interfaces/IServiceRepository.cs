using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<int> CreateAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
