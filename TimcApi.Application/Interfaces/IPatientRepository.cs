using TimcApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int patientId);
        Task<Patient> AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int patientId);
    }
}