using System.ComponentModel.DataAnnotations;

namespace TimcApi.Domain.Entities
{
    public class SACCO
    {
        [Key]
        public int AgentId { get; set; }
        public int? UserId { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; } = "";

        [MaxLength(100)]
        public string? LastName { get; set; } = "";

        [MaxLength(50)]
        public string? Phone { get; set; } = "";

        [MaxLength(500)]
        public string? Address { get; set; } = "";

        [MaxLength(100)]
        public string? City { get; set; } = "";

        [MaxLength(100)]
        public string? Country { get; set; } = "";

        [MaxLength(50)]
        public string? IdType { get; set; } = "";

        [MaxLength(100)]
        public string? IdNumber { get; set; } = "";

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        // Creator metadata
        public int? CreatedBy { get; set; }

        // New columns added
        [MaxLength(200)]
        public string? AgentName { get; set; }

        [MaxLength(100)]
        public string? RegistrationNumber { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(200)]
        public string? ContactPerson { get; set; }

        public bool IsRemoved { get; set; } = false;
        public User? User { get; set; }
        public User? Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
