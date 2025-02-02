namespace FAQ.Application.Features.Commands.DeleteResolveReport
{
    public class DeleteResolveReportCommandHandler : IRequestHandler<DeleteResolveReportCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IResponseHelper _responseHelper;
        private readonly IGRepository<ResolveReport> _resolveRepository;

        public DeleteResolveReportCommandHandler
        (
       IUnitOfWork unitOfWork, IGRepository<ResolveReport> resolveRepository,
       IResponseHelper responseHelper,
       ILogger<DeleteResolveReportCommandHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _resolveRepository = resolveRepository;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(DeleteResolveReportCommand request, CancellationToken cancellationToken)
        {

            var resolveReport = _resolveRepository.GetAll(x => x.Id == request.Id && x.State != State.Deleted).FirstOrDefault();

            if (resolveReport == null)
            {
                return _responseHelper.NotFound("Resolve Report Not Found");
            }

            resolveReport.State = State.Deleted;

            _unitOfWork.ResolveReport.Update(resolveReport);

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully(null, "Resolve Report deleted successfully");

        }

    }
}