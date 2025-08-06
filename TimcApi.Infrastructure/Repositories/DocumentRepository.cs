using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public DocumentRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<Document>> GetByPatientIdAsync(int patientId)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<Document>(
                "SELECT * FROM Documents WHERE PatientId = @patientId", new { patientId });
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Document>(
                "SELECT * FROM Documents WHERE DocumentId = @id", new { id });
        }

        public async Task<int> CreateAsync(Document doc)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO Documents (PatientId, FileName, FilePath, FileType, Description, UploadedAt, UploadedBy, Stage)
              VALUES (@PatientId, @FileName, @FilePath, @FileType, @Description, @UploadedAt, @UploadedBy, @Stage);
              SELECT CAST(SCOPE_IDENTITY() as int);", doc);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            await conn.ExecuteAsync("DELETE FROM Documents WHERE DocumentId = @id", new { id });
        }
    }

}
