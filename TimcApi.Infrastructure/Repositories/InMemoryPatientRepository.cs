using TimcApi.Application.Interfaces;
using TimcApi.Domain;
using TimcApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimcApi.Infrastructure.Repositories
{
    public class InMemoryPatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients;

        public InMemoryPatientRepository()
        {
            _patients = new List<Patient>();
            SeedDefaultUsers();
        }

        private void SeedDefaultUsers()
        {
            // Create default super admin user
            var superAdmin = new Patient(
                firstName: "Super",
                lastName: "Admin",
                email: "admin@timc.com",
                passwordHash: BCrypt.Net.BCrypt.HashPassword("Admin123!"), // Default password
                dateOfBirth: new DateTime(1980, 1, 1),
                phoneNumber: "+1234567890",
                role: Role.SuperAdmin
            );

            _patients.Add(superAdmin);
        }

        public Task<Patient> GetByIdAsync(Guid id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(patient);
        }

        public Task<Patient> GetByEmailAsync(string email)
        {
            var patient = _patients.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(patient);
        }

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
            return Task.FromResult(_patients.AsEnumerable());
        }

        public Task<Patient> AddAsync(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            _patients.Add(patient);
            return Task.FromResult(patient);
        }

        public Task<bool> ExistsAsync(string email)
        {
            var exists = _patients.Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }
    }
}