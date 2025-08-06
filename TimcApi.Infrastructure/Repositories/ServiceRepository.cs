using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ISqlConnectionFactory _connFactory;

        public ServiceRepository(ISqlConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            var conn = _connFactory.CreateConnection();
            return await conn.QueryAsync<Service>("SELECT * FROM Services WHERE IsActive = 1");
        }

        public async Task<int> CreateAsync(Service service)
        {
            var conn = _connFactory.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO Services (ServiceName, Description, IsActive) 
              VALUES (@ServiceName, @Description, 1); 
              SELECT CAST(SCOPE_IDENTITY() as int);", service);
        }

        public async Task UpdateAsync(Service service)
        {
            var conn = _connFactory.CreateConnection();
            await conn.ExecuteAsync(
                @"UPDATE Services SET 
                ServiceName = @ServiceName,
                Description = @Description,
                IsActive = @IsActive
              WHERE ServiceId = @ServiceId", service);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();
            await conn.ExecuteAsync("UPDATE Services SET IsActive = 0 WHERE ServiceId = @id", new { id });
        }
    }

}
