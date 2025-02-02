namespace FAQ.Application.Features.Commands.PutResolveReport
{
    public class PutResolveReportCommandHandler : IRequestHandler<PutResolveReportCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IResponseHelper _responseHelper;
        private readonly IGRepository<ResolveReport> _resolveRepository;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public PutResolveReportCommandHandler
        (
       IUnitOfWork unitOfWork, IGRepository<ResolveReport> resolveRepository,
       IResponseHelper responseHelper,
       Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment,
       ILogger<PutResolveReportCommandHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _resolveRepository = resolveRepository;
            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(PutResolveReportCommand request, CancellationToken cancellationToken)
        {

            var resolveReport = _resolveRepository.GetAll(x => x.Id == request.Id && x.State != State.Deleted).FirstOrDefault();

            if (resolveReport == null)
            {
                return _responseHelper.NotFound("Resolve Report Not Found");
            }

            if (_resolveRepository.GetAll(x => x.Id != request.Id && x.ResolveName == request.ResolveName).Count() > 0)
            {
                return _responseHelper.FailedToSave("Resolve Report already exist");
            }

            resolveReport.ResolveDescription = request.ResolveDescription;
            resolveReport.ResolveName = request.ResolveName;
            resolveReport.MainClassificationId = request.MainClassificationId;
            resolveReport.ServiceCatalogId = request.ServiceCatalogId;

            if (request.ImageUrl != null )
            {
                var _stream = new MemoryStream(request.ImageUrl);

                IFormFile file = new FormFile(_stream, 0, _stream.Length, request.FileName, request.FileName);
                var image = await Upload.UploadFilesNames(file, _hostingEnvironment, "Groups/" + resolveReport.Id, request.FileName);

                resolveReport.ImageUrl = image;
            }
            else
            {

                resolveReport.ImageUrl = request.AttachmentUrl == "" ? null : request.AttachmentUrl;
            }

            resolveReport.RowVersion = resolveReport.RowVersion;

            _unitOfWork.ResolveReport.Update(resolveReport);

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully(null, "Resolve Report updated successfully");

        }

    }
}