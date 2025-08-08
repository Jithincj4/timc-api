namespace TimcApi.Domain.Entities
{
    public class SACCO
    {
        public int SACCOId { get; set; }
        public int? UserId { get; set; }
        public int? FacilitatorId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ContactPersonName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
