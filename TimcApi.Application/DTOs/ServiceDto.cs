namespace TimcApi.Application.DTOs
{
    // For displaying data (GET)
    public class ServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Provider { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "draft"; // active/inactive/draft
        public string Icon { get; set; } = "fa-cogs";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // convenience for UI
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    // For creating a service (POST)
    public class CreateServiceDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Provider { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "draft";
        public string Icon { get; set; } = "fa-cogs";
        public int CategoryId { get; set; }
    }

    // For updating a service (PUT)
    public class UpdateServiceDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Provider { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "draft";
        public string Icon { get; set; } = "fa-cogs";
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; } = new CategoryDto();
    }
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Icon { get; set; } = "fa-tag";
    }
}
