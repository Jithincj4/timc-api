using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateServiceDto dto);
        Task UpdateAsync(ServiceDto dto);
        Task DeleteAsync(int id);
    }
}
