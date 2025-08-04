using System;

namespace TimcApi.Application.DTOs
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}