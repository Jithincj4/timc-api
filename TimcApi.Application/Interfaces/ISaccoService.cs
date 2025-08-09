using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface ISaccoService
    {
        Task<IEnumerable<UpdateSaccoDto>> GetAllAsync();
        Task<UpdateSaccoDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateSaccoDto dto, int createdBy);
        Task UpdateAsync(UpdateSaccoDto dto);
        Task DeleteAsync(int id);
    }
}
