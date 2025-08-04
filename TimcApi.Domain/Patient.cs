using System;
using TimcApi.Domain.Enums;

namespace TimcApi.Domain
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;
        public string? Gender { get; private set; }
        public string? Nationality { get; private set; }
        public string? IDType { get; private set; }
        public string? PassportNumber { get; private set; }
        public DateTime? IDExpiryDate { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Patient() { } // For serialization

        public Patient(string firstName, string lastName, string email, string passwordHash, 
                      DateTime dateOfBirth, string phoneNumber, string? gender = null, 
                      string? nationality = null, string? idType = null, string? passportNumber = null, 
                      DateTime? idExpiryDate = null, Role role = Role.Patient)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Gender = gender;
            Nationality = nationality;
            IDType = idType;
            PassportNumber = passportNumber;
            IDExpiryDate = idExpiryDate;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
        }
    }
}