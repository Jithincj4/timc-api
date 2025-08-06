using Microsoft.AspNetCore.Mvc;
using TimcApi.Application.DTOs;

namespace TimcApi.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetByPatientAsync(int patientId);
        Task<string> UploadAsync(UploadDocumentDto dto, int uploadedBy);
        Task<FileStreamResult?> DownloadAsync(int id);
        Task DeleteAsync(int id);
    }

}
