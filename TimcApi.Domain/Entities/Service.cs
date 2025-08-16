namespace TimcApi.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Provider { get; set; } = string.Empty;  // e.g., hospital, hotel, lab

        public decimal Price { get; set; }

        public string Currency { get; set; } = "USD";

        public string Status { get; set; } = "draft"; // active/inactive/draft

        public string Icon { get; set; } = "fa-cogs";

        public bool IsActive { get; set; } = true;

        // FK to Category
        public int CategoryId { get; set; }
        public ServiceCategory Category { get; set; } = null!;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
