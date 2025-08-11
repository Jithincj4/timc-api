namespace TimcApi.Application.DTOs
{
    public class CreateSaccoDto
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
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
        public string? AgentName { get; set; }
        public int? AgentCategory { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Location { get; set; }
        public string? ContactPerson { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class UpdateSaccoDto
    {
        public int AgentId { get; set; } // Required for identifying the record
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
        public string? AgentName { get; set; }
         public int? AgentCategory { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Location { get; set; }
        public string? ContactPerson { get; set; }
    }

}