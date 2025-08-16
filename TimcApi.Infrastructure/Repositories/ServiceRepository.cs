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

            var sql = @"
                SELECT 
                    s.Id, s.Name As ServiceName, s.Provider, s.Price, s.Currency, s.Status, s.Icon,
                    s.CategoryId, s.CreatedAt, s.UpdatedAt,
                    c.Id, c.Name, c.Description, c.Icon, c.IsActive, c.CreatedAt, c.UpdatedAt
                FROM Services s
                INNER JOIN ServiceCategories c ON s.CategoryId = c.Id;";

            return await conn.QueryAsync<Service, ServiceCategory, Service>(
                sql,
                (service, category) =>
                {
                    service.Category = category;
                    return service;
                },
                splitOn: "Id"
            );
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            var conn = _connFactory.CreateConnection();

            var sql = @"
                SELECT 
                    s.Id, s.Name As ServiceName, s.Provider, s.Price, s.Currency, s.Status, s.Icon,
                    s.CategoryId, s.CreatedAt, s.UpdatedAt,
                    c.Id, c.Name, c.Description, c.Icon, c.IsActive, c.CreatedAt, c.UpdatedAt
                FROM Services s
                INNER JOIN ServiceCategories c ON s.CategoryId = c.Id
                WHERE s.Id = @id;";

            return (await conn.QueryAsync<Service, ServiceCategory, Service>(
                sql,
                (service, category) =>
                {
                    service.Category = category;
                    return service;
                },
                new { id },
                splitOn: "Id"
            )).FirstOrDefault();
        }

        public async Task<int> CreateAsync(Service service)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
                INSERT INTO Services 
                    (Name, Provider, Price, Currency, Status, Icon, CategoryId, CreatedAt) 
                VALUES 
                    (@Name, @Provider, @Price, @Currency, @Status, @Icon, @CategoryId, SYSUTCDATETIME());
                SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, service);
        }

        public async Task UpdateAsync(Service service)
        {
            var conn = _connFactory.CreateConnection();
            var sql = @"
                UPDATE Services SET 
                    Name = @Name,
                    Provider = @Provider,
                    Price = @Price,
                    Currency = @Currency,
                    Status = @Status,
                    Icon = @Icon,
                    CategoryId = @CategoryId,
                    UpdatedAt = SYSUTCDATETIME()
                WHERE Id = @Id";

            await conn.ExecuteAsync(sql, service);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _connFactory.CreateConnection();

            // Hard delete since no IsActive column in Services
            var sql = "DELETE FROM Services WHERE Id = @id";
            await conn.ExecuteAsync(sql, new { id });
        }
    }
}
