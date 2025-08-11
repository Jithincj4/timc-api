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
            return await conn.QueryAsync<SACCO>("SELECT * FROM Agents WHERE IsRemoved = 0");
        }

        public async Task<SACCO?> GetByIdAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<SACCO>(
                "SELECT * FROM Agents WHERE AgentId = @id AND IsRemoved = 0", new { id });
        }

        public async Task<int> CreateAsync(SACCO sacco)
        {
            using var conn = _connFactory.CreateConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. Insert into Agents
                var agentSql = @"
            INSERT INTO Agents
                (UserId, FirstName, LastName, Phone, Address, City, Country, IdType, IdNumber, DateOfBirth, Gender,
                 CreatedBy, CreatedAt, AgentName, RegistrationNumber, Location, ContactPerson, IsRemoved)
            VALUES
                (@UserId, @FirstName, @LastName, @Phone, @Address, @City, @Country, @IdType, @IdNumber, @DateOfBirth, @Gender,
                 @CreatedBy, @CreatedAt, @AgentName, @RegistrationNumber, @Location, @ContactPerson, @IsRemoved);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var newAgentId = await conn.ExecuteScalarAsync<int>(agentSql, sacco, transaction);

                // 2. Insert into AgentCategoryMapping
                if (sacco.AgentCategory.HasValue)
                {
                    var mappingSql = @"
                INSERT INTO AgentCategoryMapping (AgentId, CategoryId)
                VALUES (@AgentId, @CategoryId);";

                    await conn.ExecuteAsync(mappingSql, new
                    {
                        AgentId = newAgentId,
                        CategoryId = sacco.AgentCategory.Value
                    }, transaction);
                }

                transaction.Commit();
                return newAgentId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task UpdateAsync(SACCO sacco)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
                UPDATE Agents SET
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Phone = @Phone,
                    Address = @Address,
                    City = @City,
                    Country = @Country,
                    IdType = @IdType,
                    IdNumber = @IdNumber,
                    DateOfBirth = @DateOfBirth,
                    Gender = @Gender,
                    AgentName = @AgentName,
                    RegistrationNumber = @RegistrationNumber,
                    Location = @Location,
                    ContactPerson = @ContactPerson
                WHERE AgentId = @AgentId";

            await conn.ExecuteAsync(sql, sacco);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            var sql = "UPDATE Agents SET IsRemoved = 1 WHERE AgentId = @id"; // Soft delete
            await conn.ExecuteAsync(sql, new { id });
        }
    }
}
