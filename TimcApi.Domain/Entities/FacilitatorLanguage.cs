using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Domain.Entities
{
    // Junction table classes
    public class FacilitatorLanguage
    {
        public int FacilitatorId { get; set; }
        public int LanguageId { get; set; }

        // Optional navigation properties
        public Facilitator? Facilitator { get; set; }
        public Language? Language { get; set; }
    }
}
