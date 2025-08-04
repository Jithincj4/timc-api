using TimcApi.Application.Interfaces;
using TimcApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimcApi.Infrastructure.Repositories
{
    public class InMemoryRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly List<RefreshToken> _refreshTokens;

        public InMemoryRefreshTokenRepository()
        {
            _refreshTokens = new List<RefreshToken>();
        }

        public Task<RefreshToken> AddAsync(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new ArgumentNullException(nameof(refreshToken));

            _refreshTokens.Add(refreshToken);
            return Task.FromResult(refreshToken);
        }

        public Task<RefreshToken?> GetByTokenAsync(string token)
        {
            var refreshToken = _refreshTokens.FirstOrDefault(rt => rt.Token == token);
            return Task.FromResult(refreshToken);
        }

        public Task<IEnumerable<RefreshToken>> GetByPatientIdAsync(Guid patientId)
        {
            var tokens = _refreshTokens.Where(rt => rt.PatientId == patientId);
            return Task.FromResult(tokens);
        }

        public Task RevokeAsync(RefreshToken refreshToken)
        {
            if (refreshToken != null)
            {
                refreshToken.Revoke();
            }
            return Task.CompletedTask;
        }

        public Task RevokeAllByPatientIdAsync(Guid patientId)
        {
            var patientTokens = _refreshTokens.Where(rt => rt.PatientId == patientId);
            foreach (var token in patientTokens)
            {
                token.Revoke();
            }
            return Task.CompletedTask;
        }

        public Task<bool> IsValidAsync(string token)
        {
            var refreshToken = _refreshTokens.FirstOrDefault(rt => rt.Token == token);
            var isValid = refreshToken?.IsActive ?? false;
            return Task.FromResult(isValid);
        }
    }
}