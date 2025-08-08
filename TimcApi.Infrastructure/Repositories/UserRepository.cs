using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;
using TimcApi.Infrastructure.Utility;

namespace TimcApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public UserRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE UserId = @id", new { id });
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email = @email", new { email });
        }

        public async Task<int> CreateAsync(User user)
        {
            user.PasswordHash = PasswordHasher.Hash(user.Password);
            var conn = _connFactory.CreateConnection();

            var sql = @"
            INSERT INTO Users (Username, Email, PasswordHash, RoleId, IsActive, CreatedAt)
            VALUES (@Username, @Email, @PasswordHash, @RoleId, @IsActive, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

            return await conn.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            UPDATE Users SET
                Username = @Username,
                Email = @Email,
                RoleId = @RoleId,
                IsActive = @IsActive
            WHERE UserId = @UserId";
            await conn.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            var sql = "UPDATE Users SET IsActive = 0 WHERE UserId = @id";
            await conn.ExecuteAsync(sql, new { id });
        }
    }
}
