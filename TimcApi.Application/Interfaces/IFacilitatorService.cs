using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IFacilitatorService
    {
        Task<IEnumerable<FacilitatorDto>> GetAllAsync();
        Task<FacilitatorDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUserAndFacilitator dto);
        Task UpdateAsync(FacilitatorDto dto);
        Task DeleteAsync(int id);
    }
}
