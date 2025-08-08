using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Domain.Entities
{
    public class FacilitatorSpecialization
    {
        public int FacilitatorId { get; set; }
        public int SpecializationId { get; set; }

        // Optional navigation properties
        public Facilitator? Facilitator { get; set; }
        public Specialization? Specialization { get; set; }
    }
}
