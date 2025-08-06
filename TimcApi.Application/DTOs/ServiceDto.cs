using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Application.DTOs
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateServiceDto
    {
        public string ServiceName { get; set; }
        public string? Description { get; set; }
    }
}
