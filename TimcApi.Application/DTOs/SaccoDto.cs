namespace TimcApi.Application.DTOs
{
    /// <summary>
    /// DTO used for returning SACCO (Agent) data to clients.
    /// </summary>
    public class SaccoDto
    {
        public int AgentId { get; set; }
        public int? UserId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public string? IdType { get; set; }
        public string? IdNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        // Newly added agent fields
        public string? AgentName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Location { get; set; }
        public string? ContactPerson { get; set; }

        public bool IsRemoved { get; set; }
    }
}
