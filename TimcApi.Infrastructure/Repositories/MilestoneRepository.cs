using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public MilestoneRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<Milestone>> GetByPatientIdAsync(int patientId)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<Milestone>(
                "SELECT * FROM Milestones WHERE PatientId = @patientId", new { patientId });
        }

        public async Task<int> CreateAsync(Milestone milestone)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            INSERT INTO Milestones (PatientId, Stage, IsCompleted, CompletedOn, Remarks)
            VALUES (@PatientId, @Stage, @IsCompleted, @CompletedOn, @Remarks);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, milestone);
        }

        public async Task UpdateAsync(Milestone milestone)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
            UPDATE Milestones SET
                IsCompleted = @IsCompleted,
                CompletedOn = @CompletedOn,
                Remarks = @Remarks
            WHERE MilestoneId = @MilestoneId";

            await conn.ExecuteAsync(sql, milestone);
        }
    }

}
