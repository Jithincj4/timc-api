using System;
using TimcApi.Domain.Enums;

namespace TimcApi.Application.DTOs
{
    public class PatientResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? IDType { get; set; }
        public string? PassportNumber { get; set; }
        public DateTime? IDExpiryDate { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}