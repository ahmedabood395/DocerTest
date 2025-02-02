namespace FAQ.Application.Features.Commands.DeleteResolveReport
{
    public class DeleteResolveReportCommand:IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
