using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Domain.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string WhatsApp { get; set; }
        public string MedicalCondition { get; set; }
        public string Symptoms { get; set; }
        public string DurationOfIllness { get; set; }
        public string Medications { get; set; }
        public string Allergies { get; set; }
        public string PreferredSpecialties { get; set; }
        public bool VisaRequired { get; set; }
        public DateTime TravelDate { get; set; }
        public string PreferredCity { get; set; }
        public bool IsSelfFunded { get; set; }
        public string SponsorName { get; set; }
        public string SponsorContact { get; set; }
        public bool HasInsurance { get; set; }
        public int? SACCOId { get; set; }
        public string MemberId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
