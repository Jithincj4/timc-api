using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Domain.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string? Description { get; set; }
        public DateTime UploadedAt { get; set; }
        public int UploadedBy { get; set; }
        public string? Stage { get; set; } // Optional: "Visa Issued", "Consultation", etc.
    }
}
