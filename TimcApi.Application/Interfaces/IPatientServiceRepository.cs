using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IPatientServiceRepository
    {
        Task<int> AssignAsync(PatientService ps);
        Task<IEnumerable<PatientService>> GetByPatientIdAsync(int patientId);
    }
}
