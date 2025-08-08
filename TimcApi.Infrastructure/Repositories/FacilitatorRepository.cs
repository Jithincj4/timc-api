using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TimcApi.Application.Interfaces;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class FacilitatorRepository : IFacilitatorRepository
    {
        private readonly ISqlConnectionFactory _connection;

        public FacilitatorRepository(ISqlConnectionFactory connFactory)
        {
            _connection = connFactory;
        }

        public async Task<IEnumerable<Facilitator>> GetAllAsync()
        {
            using var connection = _connection.CreateConnection();

            // Step 1: Get facilitators with their user info
            const string sql = @"
        SELECT f.*, u.UserId, u.Username, u.Email, u.PasswordHash, u.RoleId, u.IsActive, u.CreatedAt
        FROM Facilitators f
        JOIN Users u ON f.UserId = u.UserId";

            var facilitators = await connection.QueryAsync<Facilitator, User, Facilitator>(
                sql,
                (facilitator, user) =>
                {
                    facilitator.User = user;
                    return facilitator;
                },
                splitOn: "UserId"
            );

            var facilitatorList = facilitators.ToList();
            if (!facilitatorList.Any())
                return facilitatorList;

            // Step 2: Get all languages for all facilitators
            const string languagesSql = @"
        SELECT fl.FacilitatorId, l.*
        FROM FacilitatorLanguages fl
        JOIN LanguageMaster l ON fl.LanguageId = l.LanguageId";

            var languagesLookup = (await connection.QueryAsync<int, Language, (int FacilitatorId, Language Lang)>(
                languagesSql,
                (facilitatorId, language) => (facilitatorId, language),
                splitOn: "LanguageId"
            ))
            .GroupBy(x => x.FacilitatorId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Lang).ToList());

            // Step 3: Get all specializations for all facilitators
            const string specializationsSql = @"
        SELECT fs.FacilitatorId, s.*
        FROM FacilitatorSpecializations fs
        JOIN Specializations s ON fs.SpecializationId = s.SpecializationId";

            var specializationsLookup = (await connection.QueryAsync<int, Specialization, (int FacilitatorId, Specialization Spec)>(
                specializationsSql,
                (facilitatorId, spec) => (facilitatorId, spec),
                splitOn: "SpecializationId"
            ))
            .GroupBy(x => x.FacilitatorId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Spec).ToList());

            // Step 4: Assign related data
            foreach (var facilitator in facilitatorList)
            {
                if (languagesLookup.TryGetValue(facilitator.FacilitatorId, out var langs))
                    facilitator.Languages = langs;

                if (specializationsLookup.TryGetValue(facilitator.FacilitatorId, out var specs))
                    facilitator.Specializations = specs;
            }

            return facilitatorList;
        }


        public async Task<Facilitator?> GetByIdAsync(int id)
        {
            using var connection = _connection.CreateConnection();

            // Get facilitator with user info
            const string facilitatorSql = @"
            SELECT f.*, u.*
            FROM Facilitators f
            JOIN Users u ON f.UserId = u.UserId
            WHERE f.FacilitatorId = @Id";

            var facilitator = await connection.QueryAsync<Facilitator, User, Facilitator>(
                facilitatorSql,
                (facilitator, user) =>
                {
                    facilitator.User = user;
                    return facilitator;
                },
                new { Id = id },
                splitOn: "UserId"
            ).ContinueWith(task => task.Result.FirstOrDefault());

            if (facilitator == null) return null;

            // Get languages
            const string languagesSql = @"
            SELECT l.*
            FROM Languages l
            JOIN FacilitatorLanguages fl ON l.LanguageId = fl.LanguageId
            WHERE fl.FacilitatorId = @Id";

            facilitator.Languages = (await connection.QueryAsync<Language>(languagesSql, new { Id = id })).ToList();

            // Get specializations
            const string specializationsSql = @"
            SELECT s.*
            FROM Specializations s
            JOIN FacilitatorSpecializations fs ON s.SpecializationId = fs.SpecializationId
            WHERE fs.FacilitatorId = @Id";

            facilitator.Specializations = (await connection.QueryAsync<Specialization>(specializationsSql, new { Id = id })).ToList();

            return facilitator;
        }

        public async Task<int> CreateAsync(Facilitator facilitator)
        {
            using var connection = _connection.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insert facilitator
                const string facilitatorSql = @"
            INSERT INTO Facilitators (
                UserId, FirstName, LastName, Phone, Address, City, Country, 
                IdType, IdNumber, DateOfBirth, Gender, CreatedBy, CreatedAt
            )
            VALUES (
                @UserId, @FirstName, @LastName, @Phone, @Address, @City, @Country, 
                @IdType, @IdNumber, @DateOfBirth, @Gender, @CreatedBy, @CreatedAt
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

                facilitator.FacilitatorId = await connection.QuerySingleAsync<int>(
                    facilitatorSql,
                    facilitator,
                    transaction
                );

                // Insert languages
                if (facilitator.Languages?.Any() == true)
                {
                    const string languageSql = @"
                INSERT INTO FacilitatorLanguages (FacilitatorId, LanguageId)
                VALUES (@FacilitatorId, @LanguageId)";

                    var languageParams = facilitator.Languages
                        .Select(l => new {
                            FacilitatorId = facilitator.FacilitatorId,
                            LanguageId = l.LanguageId
                        });

                    await connection.ExecuteAsync(
                        languageSql,
                        languageParams,
                        transaction
                    );
                }

                // Insert specializations
                if (facilitator.Specializations?.Any() == true)
                {
                    const string specializationSql = @"
                INSERT INTO FacilitatorSpecializations (FacilitatorId, SpecializationId)
                VALUES (@FacilitatorId, @SpecializationId)";

                    var specializationParams = facilitator.Specializations
                        .Select(s => new {
                            FacilitatorId = facilitator.FacilitatorId,
                            SpecializationId = s.SpecializationId
                        });

                    await connection.ExecuteAsync(
                        specializationSql,
                        specializationParams,
                        transaction
                    );
                }

                transaction.Commit();
                return facilitator.FacilitatorId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task UpdateAsync(Facilitator facilitator)
        {
            using var connection = _connection.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Update facilitator basic details
                const string updateFacilitatorSql = @"
            UPDATE Facilitators
            SET 
                UserId = @UserId,
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
                CreatedBy = @CreatedBy
            WHERE FacilitatorId = @FacilitatorId";

                await connection.ExecuteAsync(updateFacilitatorSql, facilitator, transaction);

                // Delete existing specializations
                const string deleteSpecializationsSql = @"
            DELETE FROM FacilitatorSpecializations 
            WHERE FacilitatorId = @FacilitatorId";

                await connection.ExecuteAsync(deleteSpecializationsSql, new { facilitator.FacilitatorId }, transaction);

                // Insert new specializations
                if (facilitator.Specializations?.Any() == true)
                {
                    const string insertSpecializationsSql = @"
                INSERT INTO FacilitatorSpecializations (FacilitatorId, SpecializationId)
                VALUES (@FacilitatorId, @SpecializationId)";

                    var specializationParams = facilitator.Specializations
                        .Select(s => new {
                            FacilitatorId = facilitator.FacilitatorId,
                            SpecializationId = s.SpecializationId
                        });

                    await connection.ExecuteAsync(
                        insertSpecializationsSql,
                        specializationParams,
                        transaction
                    );
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connection.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Update facilitator basic details
                const string updateFacilitatorSql = @"
            UPDATE Facilitators
            SET 
                IsRemoved = 1,
            WHERE FacilitatorId = @id";

                await connection.ExecuteAsync(updateFacilitatorSql, id, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
