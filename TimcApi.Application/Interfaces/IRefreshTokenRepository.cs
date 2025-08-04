using TimcApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimcApi.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetByPatientIdAsync(Guid patientId);
        Task RevokeAsync(RefreshToken refreshToken);
        Task RevokeAllByPatientIdAsync(Guid patientId);
        Task<bool> IsValidAsync(string token);
    }
}