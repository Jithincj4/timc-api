namespace TimcApi.Domain.Entities
{
    public class ServiceCategory
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Icon { get; set; } = "fa-tag";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
