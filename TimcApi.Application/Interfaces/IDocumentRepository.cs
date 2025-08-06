using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetByPatientIdAsync(int patientId);
        Task<Document?> GetByIdAsync(int id);
        Task<int> CreateAsync(Document doc);
        Task DeleteAsync(int id);
    }

}
