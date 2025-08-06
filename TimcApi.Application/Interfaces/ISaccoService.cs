using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface ISaccoService
    {
        Task<IEnumerable<SaccoDto>> GetAllAsync();
        Task<SaccoDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateSaccoDto dto, int createdBy);
        Task UpdateAsync(SaccoDto dto);
        Task DeleteAsync(int id);
    }
}
