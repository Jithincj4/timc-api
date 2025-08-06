using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class SaccoRepository : ISaccoRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public SaccoRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<SACCO>> GetAllAsync()
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<SACCO>("SELECT * FROM SACCOs");
        }

        public async Task<SACCO?> GetByIdAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<SACCO>(
                "SELECT * FROM SACCOs WHERE SACCOId = @id", new { id });
        }

        public async Task<int> CreateAsync(SACCO sacco)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            INSERT INTO SACCOs (Name, Address, Phone, Email, CreatedBy, CreatedAt)
            VALUES (@Name, @Address, @Phone, @Email, @CreatedBy, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, sacco);
        }

        public async Task UpdateAsync(SACCO sacco)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            UPDATE SACCOs SET
                Name = @Name,
                Address = @Address,
                Phone = @Phone,
                Email = @Email
            WHERE SACCOId = @SACCOId";
            await conn.ExecuteAsync(sql, sacco);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            var sql = "DELETE FROM SACCOs WHERE SACCOId = @id";  // Or soft delete logic
            await conn.ExecuteAsync(sql, new { id });
        }
    }

}
