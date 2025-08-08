using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Application.DTOs
{
    public class SpecializationDto
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
