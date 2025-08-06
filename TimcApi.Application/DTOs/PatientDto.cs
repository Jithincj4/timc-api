using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimcApi.Application.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool VisaRequired { get; set; }
        public DateTime TravelDate { get; set; }
        public string PreferredCity { get; set; }
        public bool IsSelfFunded { get; set; }
        public string? SponsorName { get; set; }
    }
    public class CreatePatientDto
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool VisaRequired { get; set; }
        public DateTime TravelDate { get; set; }
        public string PreferredCity { get; set; }
        public bool IsSelfFunded { get; set; }
        public string? SponsorName { get; set; }
    }
}
