using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUserDto dto);
        Task UpdateAsync(UserDto dto);
        Task DeleteAsync(int id);
        Task<UserDto?> GetByEmailAsync(string email);
    }
}
