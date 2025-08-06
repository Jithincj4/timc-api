using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
        Task UpdatePatientAsync(PatientDto dto);
        Task DeletePatientAsync(int id);
    }
}