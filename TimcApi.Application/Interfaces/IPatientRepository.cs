using TimcApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimcApi.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(Guid id);
        Task<Patient> GetByEmailAsync(string email);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> AddAsync(Patient patient);
        Task<bool> ExistsAsync(string email);
    }
}