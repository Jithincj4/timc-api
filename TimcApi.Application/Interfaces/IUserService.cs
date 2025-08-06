using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
