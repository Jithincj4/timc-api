using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class FacilitatorRepository : IFacilitatorRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public FacilitatorRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<Facilitator>> GetAllAsync()
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<Facilitator>("SELECT * FROM Facilitators");
        }

        public async Task<Facilitator?> GetByIdAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Facilitator>(
                "SELECT * FROM Facilitators WHERE FacilitatorId = @id", new { id });
        }

        public async Task<int> CreateAsync(Facilitator facilitator)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            INSERT INTO Facilitators (FullName, Email, Phone, UserId, SACCOId)
            VALUES (@FullName, @Email, @Phone, @UserId, @SACCOId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, facilitator);
        }

        public async Task UpdateAsync(Facilitator facilitator)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            UPDATE Facilitators SET
                FullName = @FullName,
                Email = @Email,
                Phone = @Phone,
                SACCOId = @SACCOId
            WHERE FacilitatorId = @FacilitatorId";

            await conn.ExecuteAsync(sql, facilitator);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            var sql = "DELETE FROM Facilitators WHERE FacilitatorId = @id";
            await conn.ExecuteAsync(sql, new { id });
        }
    }

}
