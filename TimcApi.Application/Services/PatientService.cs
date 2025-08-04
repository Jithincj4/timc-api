using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Domain;
using TimcApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TimcApi.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public PatientService(IPatientRepository patientRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _patientRepository = patientRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<PatientResponseDto> RegisterAsync(RegisterPatientDto registerDto)
        {
            // Check if patient already exists
            if (await _patientRepository.ExistsAsync(registerDto.Email))
            {
                throw new InvalidOperationException("Patient with this email already exists");
            }

            // Hash password using BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create patient entity (defaults to Patient role)
            var patient = new Patient(
                registerDto.FirstName,
                registerDto.LastName,
                registerDto.Email,
                passwordHash,
                registerDto.DateOfBirth,
                registerDto.PhoneNumber,
                registerDto.Gender,
                registerDto.Nationality,
                registerDto.IDType,
                registerDto.PassportNumber,
                registerDto.IDExpiryDate
            );

            // Save patient
            var savedPatient = await _patientRepository.AddAsync(patient);

            // Return response DTO
            return new PatientResponseDto
            {
                Id = savedPatient.Id,
                FirstName = savedPatient.FirstName,
                LastName = savedPatient.LastName,
                Email = savedPatient.Email,
                DateOfBirth = savedPatient.DateOfBirth,
                PhoneNumber = savedPatient.PhoneNumber,
                Gender = savedPatient.Gender,
                Nationality = savedPatient.Nationality,
                IDType = savedPatient.IDType,
                PassportNumber = savedPatient.PassportNumber,
                IDExpiryDate = savedPatient.IDExpiryDate,
                Role = savedPatient.Role,
                CreatedAt = savedPatient.CreatedAt
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Find patient by email
            var patient = await _patientRepository.GetByEmailAsync(loginDto.Email);
            if (patient == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Verify password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, patient.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Generate tokens
            var accessToken = GenerateAccessToken(patient);
            var refreshToken = GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddHours(1);

            // Store refresh token
            var refreshTokenEntity = new RefreshToken(refreshToken, patient.Id, DateTime.UtcNow.AddDays(7));
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                TokenType = "Bearer"
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshTokenDto.RefreshToken);
            
            if (refreshToken == null || !refreshToken.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            var patient = await _patientRepository.GetByIdAsync(refreshToken.PatientId);
            if (patient == null)
            {
                throw new UnauthorizedAccessException("Patient not found");
            }

            // Revoke the old refresh token
            await _refreshTokenRepository.RevokeAsync(refreshToken);

            // Generate new tokens
            var newAccessToken = GenerateAccessToken(patient);
            var newRefreshToken = GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddHours(1);

            // Store new refresh token
            var newRefreshTokenEntity = new RefreshToken(newRefreshToken, patient.Id, DateTime.UtcNow.AddDays(7));
            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

            return new LoginResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = expiresAt,
                TokenType = "Bearer"
            };
        }

        public async Task RevokeTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshTokenDto.RefreshToken);
            if (refreshToken != null)
            {
                await _refreshTokenRepository.RevokeAsync(refreshToken);
            }
        }

        public async Task RevokeAllTokensAsync(Guid patientId)
        {
            await _refreshTokenRepository.RevokeAllByPatientIdAsync(patientId);
        }

        public async Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return patients.Select(p => new PatientResponseDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                PhoneNumber = p.PhoneNumber,
                Gender = p.Gender,
                Nationality = p.Nationality,
                IDType = p.IDType,
                PassportNumber = p.PassportNumber,
                IDExpiryDate = p.IDExpiryDate,
                Role = p.Role,
                CreatedAt = p.CreatedAt
            });
        }

        private string GenerateAccessToken(Patient patient)
        {
            // Mock JWT-like token structure including role
            var header = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("{\"alg\":\"HS256\",\"typ\":\"JWT\"}"));
            var payload = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{{\"sub\":\"{patient.Id}\",\"email\":\"{patient.Email}\",\"role\":\"{patient.Role}\",\"exp\":{DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()}}}"));
            var signature = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("mock_signature"));
            
            return $"{header}.{payload}.{signature}";
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}