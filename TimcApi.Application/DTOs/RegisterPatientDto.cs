using System;
using System.ComponentModel.DataAnnotations;

namespace TimcApi.Application.DTOs
{
    public class RegisterPatientDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? IDType { get; set; }
        public string? PassportNumber { get; set; }
        public DateTime? IDExpiryDate { get; set; }
    }
}