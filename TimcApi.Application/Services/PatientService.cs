using AutoMapper;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _repo.GetByIdAsync(id);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            patient.CreatedAt = DateTime.UtcNow;

            var added = await _repo.AddAsync(patient);
            return _mapper.Map<PatientDto>(added);
        }

        public async Task UpdatePatientAsync(PatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            await _repo.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}