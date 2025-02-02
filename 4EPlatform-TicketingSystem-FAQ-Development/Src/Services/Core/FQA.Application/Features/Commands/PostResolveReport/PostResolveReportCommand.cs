namespace FAQ.Application.Features.Commands.PostResolveReport
{
    public class PostResolveReportCommand : IRequest<ResponseDTO>
    {
        public string ResolveName { get; set; }
        public string ResolveDescription { get; set; }
        public Guid MainClassificationId { get; set; }
        public Guid ServiceCatalogId { get; set; }
        public byte[]? ImageUrl { get; set; }
        public string? FileName { get; set; }
    }
}
