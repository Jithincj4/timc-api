using Dapper;
using TimcApi.Application.DTOs;
using TimcApi.Application.Interfaces;
using TimcApi.Infrastructure.Common;
using TimcApi.Infrastructure.Utility;

namespace TimcApi.Infrastructure.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly ISqlConnectionFactory _connFactory;
        private readonly IJwtService _jwt;

        public AuthService(ISqlConnectionFactory connFactory, IJwtService jwt)
        {
            _connFactory = connFactory;
            _jwt = jwt;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            using var conn = _connFactory.CreateConnection();
            var hash = PasswordHasher.Hash(dto.Password);

            var sql = @"
            INSERT INTO Users (Username, Email, PasswordHash, RoleId)
            VALUES (@Username, @Email, @PasswordHash, @RoleId)";

            var result = await conn.ExecuteAsync(sql, new
            {
                dto.Username,
                dto.Email,
                PasswordHash = hash,
                dto.RoleId
            });

            return result > 0;
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto)
        {
            using var conn = _connFactory.CreateConnection();

            var sql = @"
            SELECT u.UserId, u.Username, u.PasswordHash, r.RoleName
            FROM Users u
            JOIN Roles r ON u.RoleId = r.RoleId
            WHERE u.Username = @Username AND u.IsActive = 1";

            var user = await conn.QueryFirstOrDefaultAsync(sql, new { dto.Username });

            if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwt.GenerateToken(user.Username, user.RoleName);

            return new AuthResponseDto
            {
                Username = user.Username,
                Role = user.RoleName,
                Token = token
            };
        }
    }
}
