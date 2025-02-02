
namespace FAQ.Application.Features.Commands.PutResolveReport
{
    public class PutResolveReportCommand : IRequest<ResponseDTO>
    {
        public string ResolveName { get; set; }
        public string ResolveDescription { get; set; }
        public Guid MainClassificationId { get; set; }
        public Guid ServiceCatalogId { get; set; }

        public Guid Id { get; set; }
        public byte[]? ImageUrl { get; set; }
        public string? FileName { get; set; }
        public string? AttachmentUrl { get; set; }
    }
}
