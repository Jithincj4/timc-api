using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Domain.Entities
{
    public class Milestone
    {
        public int MilestoneId { get; set; }
        public int PatientId { get; set; }
        public string Stage { get; set; }            // e.g., "Visa Issued"
        public bool IsCompleted { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string? Remarks { get; set; }
    }
}
