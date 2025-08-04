using TimcApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimcApi.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientResponseDto> RegisterAsync(RegisterPatientDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task RevokeTokenAsync(RefreshTokenDto refreshTokenDto);
        Task RevokeAllTokensAsync(Guid patientId);
        Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync();
    }
}