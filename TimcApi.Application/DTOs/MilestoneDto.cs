using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Application.DTOs
{
    public class MilestoneDto
    {
        public int MilestoneId { get; set; }
        public int PatientId { get; set; }
        public string Stage { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string? Remarks { get; set; }
    }
    public class CreateMilestoneDto
    {
        public int PatientId { get; set; }
        public string Stage { get; set; }
        public string? Remarks { get; set; }
    }
    public class UpdateMilestoneDto
    {
        public int MilestoneId { get; set; }
        public bool IsCompleted { get; set; }
        public string? Remarks { get; set; }
    }
}
