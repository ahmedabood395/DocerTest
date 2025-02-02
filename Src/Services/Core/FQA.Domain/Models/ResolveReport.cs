namespace FAQ.Domain.Models
{
    public class ResolveReport : BaseEntity
    {
        public string ResolveName { get; set; }
        public string ResolveDescription { get; set; }
        public Guid MainClassificationId { get; set; }
        public Guid ServiceCatalogId { get; set; }
        public string? ImageUrl { get; set; } = null;
    }
}
