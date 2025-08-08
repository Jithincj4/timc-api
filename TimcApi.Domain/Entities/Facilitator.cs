using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        // Optional: Include related data if needed
        public User? User { get; set; }
        public User? Creator { get; set; }
        public List<Language> Languages { get; set; } = new();
        public List<Specialization> Specializations { get; set; } = new();
    }

    // Junction table classes
    public class FacilitatorLanguage
    {
        public int FacilitatorId { get; set; }
        public int LanguageId { get; set; }

        // Optional navigation properties
        public Facilitator? Facilitator { get; set; }
        public Language? Language { get; set; }
    }

    public class FacilitatorSpecialization
    {
        public int FacilitatorId { get; set; }
        public int SpecializationId { get; set; }

        // Optional navigation properties
        public Facilitator? Facilitator { get; set; }
        public Specialization? Specialization { get; set; }
    }

    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }

    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public int CategoryId { get; set; }
    }
}
