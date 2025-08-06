using Microsoft.AspNetCore.Http;

namespace TimcApi.Application.DTOs
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string? Description { get; set; }
        public string? Stage { get; set; }
        public DateTime UploadedAt { get; set; }
    }
    public class UploadDocumentDto
    {
        public int PatientId { get; set; }
        public string? Description { get; set; }
        public string? Stage { get; set; }
        public IFormFile File { get; set; }
    }
}
