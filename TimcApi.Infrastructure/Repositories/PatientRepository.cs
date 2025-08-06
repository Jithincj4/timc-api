using Dapper;
using TimcApi.Application.Interfaces;
using TimcApi.Domain;
using TimcApi.Domain.Entities;
using TimcApi.Infrastructure.Common;

namespace TimcApi.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public PatientRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM Patients";
            return await connection.QueryAsync<Patient>(sql);
        }

        public async Task<Patient?> GetByIdAsync(int patientId)
        {
            using var connection = _connectionFactory.CreateConnection();
            string sql = "SELECT * FROM Patients WHERE PatientId = @PatientId";
            return await connection.QueryFirstOrDefaultAsync<Patient>(sql, new { PatientId = patientId });
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            using var connection = _connectionFactory.CreateConnection();

            string sql = @"
                INSERT INTO Patients 
                (FullName, Gender, DateOfBirth, Nationality, PassportNumber, PassportExpiryDate, MaritalStatus,
                 Email, Phone, Address, WhatsApp, MedicalCondition, Symptoms, DurationOfIllness, Medications, 
                 Allergies, PreferredSpecialties, VisaRequired, TravelDate, PreferredCity, IsSelfFunded, 
                 SponsorName, SponsorContact, HasInsurance, SACCOId, MemberId, UserId, CreatedAt)
                VALUES 
                (@FullName, @Gender, @DateOfBirth, @Nationality, @PassportNumber, @PassportExpiryDate, @MaritalStatus,
                 @Email, @Phone, @Address, @WhatsApp, @MedicalCondition, @Symptoms, @DurationOfIllness, @Medications,
                 @Allergies, @PreferredSpecialties, @VisaRequired, @TravelDate, @PreferredCity, @IsSelfFunded,
                 @SponsorName, @SponsorContact, @HasInsurance, @SACCOId, @MemberId, @UserId, @CreatedAt);

                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var patientId = await connection.ExecuteScalarAsync<int>(sql, patient);
            patient.PatientId = patientId;
            return patient;
        }

        public async Task UpdateAsync(Patient patient)
        {
            using var connection = _connectionFactory.CreateConnection();

            string sql = @"
                UPDATE Patients SET
                    FullName = @FullName,
                    Gender = @Gender,
                    DateOfBirth = @DateOfBirth,
                    Nationality = @Nationality,
                    PassportNumber = @PassportNumber,
                    PassportExpiryDate = @PassportExpiryDate,
                    MaritalStatus = @MaritalStatus,
                    Email = @Email,
                    Phone = @Phone,
                    Address = @Address,
                    WhatsApp = @WhatsApp,
                    MedicalCondition = @MedicalCondition,
                    Symptoms = @Symptoms,
                    DurationOfIllness = @DurationOfIllness,
                    Medications = @Medications,
                    Allergies = @Allergies,
                    PreferredSpecialties = @PreferredSpecialties,
                    VisaRequired = @VisaRequired,
                    TravelDate = @TravelDate,
                    PreferredCity = @PreferredCity,
                    IsSelfFunded = @IsSelfFunded,
                    SponsorName = @SponsorName,
                    SponsorContact = @SponsorContact,
                    HasInsurance = @HasInsurance,
                    SACCOId = @SACCOId,
                    MemberId = @MemberId,
                    UserId = @UserId
                WHERE PatientId = @PatientId;
            ";

            await connection.ExecuteAsync(sql, patient);
        }

        public async Task DeleteAsync(int patientId)
        {
            using var connection = _connectionFactory.CreateConnection();
            string sql = "DELETE FROM Patients WHERE PatientId = @PatientId";
            await connection.ExecuteAsync(sql, new { PatientId = patientId });
        }
    }
}