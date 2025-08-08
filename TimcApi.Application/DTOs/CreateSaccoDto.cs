namespace TimcApi.Application.DTOs
{
    public class CreateSaccoDto
    {
        public string? Name { get; set; }
        public string? ContactPersonName { get; set; }

        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public int? UserId { get; set; }
        public int? FacilitatorId { get; set; }
    }
}