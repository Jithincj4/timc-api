using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class PatientServiceRepository : IPatientServiceRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public PatientServiceRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<int> AssignAsync(PatientService ps)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO PatientServices (PatientId, ServiceId, Status, CreatedAt)
              VALUES (@PatientId, @ServiceId, @Status, @CreatedAt);
              SELECT CAST(SCOPE_IDENTITY() as int);", ps);
        }

        public async Task<IEnumerable<PatientService>> GetByPatientIdAsync(int patientId)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<PatientService>(
                "SELECT * FROM PatientServices WHERE PatientId = @patientId", new { patientId });
        }
    }

}
