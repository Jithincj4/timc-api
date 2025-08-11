namespace TimcApi.Domain.Entities
{
    public class Facilitator
    {
        public int FacilitatorId { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? IdType { get; set; }

        public string? IdNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public string? OrganisationName { get; set; }

        public int? YearsOfExperience { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }
        public bool IsRemoved { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        // Optional: Include related data if needed
        public User? User { get; set; }
        public User? Creator { get; set; }
        public List<Language> Languages { get; set; } = new();
        public List<Specialization> Specializations { get; set; } = new();
    }
}
