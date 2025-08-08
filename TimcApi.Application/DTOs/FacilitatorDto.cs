using System.ComponentModel.DataAnnotations;

namespace TimcApi.Application.DTOs
{
    public class FacilitatorDto
    {
        public int FacilitatorId { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

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

        public List<LanguageDto> Languages { get; set; } = new();
        public List<SpecializationDto> Specializations { get; set; } = new();
    }

    public class CreateFacilitatorDto
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(50)]
        public string? IdType { get; set; }

        [StringLength(100)]
        public string? IdNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public int? CreatedBy { get; set; }

        public List<int> LanguageIds { get; set; } = new();
        public List<int> SpecializationIds { get; set; } = new();
    }
    public class UpdateFacilitatorDto
    {
        public int FacilitatorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(50)]
        public string? IdType { get; set; }

        [StringLength(100)]
        public string? IdNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public int? CreatedBy { get; set; }

        public List<int> LanguageIds { get; set; } = new();
        public List<int> SpecializationIds { get; set; } = new();
    }
}
