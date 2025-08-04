using System;
using System.Text.RegularExpressions;

namespace TimcApi.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private Email() { } // For serialization

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be null or empty", nameof(value));

            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email format", nameof(value));

            Value = value.ToLowerInvariant();
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new Email(value);

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Email email && Value == email.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}