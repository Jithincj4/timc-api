using System;

namespace TimcApi.Domain
{
    public class PatientDocument
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public string FileName { get; private set; } = string.Empty;
        public string FilePath { get; private set; } = string.Empty;
        public string ContentType { get; private set; } = string.Empty;
        public long FileSize { get; private set; }
        public DateTime UploadedAt { get; private set; }

        private PatientDocument() { } // For serialization

        public PatientDocument(Guid patientId, string fileName, string filePath, string contentType, long fileSize)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            FileSize = fileSize;
            UploadedAt = DateTime.UtcNow;
        }
    }
}