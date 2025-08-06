namespace TimcApi.Domain.Entities
{
    public class Facilitator
    {
        public int FacilitatorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public int? SACCOId { get; set; }
    }
}
