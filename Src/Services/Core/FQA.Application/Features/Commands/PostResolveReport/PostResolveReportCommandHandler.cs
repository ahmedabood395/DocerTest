

namespace FAQ.Application.Features.Commands.PostResolveReport
{
    public class PosttResolveReportCommandHandler : IRequestHandler<PostResolveReportCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        private readonly IGRepository<ResolveReport> _resolveRepository;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public PosttResolveReportCommandHandler
        (
       IUnitOfWork unitOfWork, IGRepository<ResolveReport> resolveRepository,
       IResponseHelper responseHelper,
       Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment,
       ILogger<PosttResolveReportCommandHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _resolveRepository = resolveRepository;

            _responseHelper = responseHelper;
        }
        public async Task<ResponseDTO> Handle(PostResolveReportCommand request, CancellationToken cancellationToken)
        {
            var oldResolve = _resolveRepository.GetFirst(x => x.ResolveName == request.ResolveName);
            if (oldResolve is not null)
            {
                return _responseHelper.FailedToSave("Resolve Report already exist");
            }

            var resolveReport = new ResolveReport()
            {
                ResolveName = request.ResolveName,
                ResolveDescription = request.ResolveDescription,
                MainClassificationId = request.MainClassificationId,
                ServiceCatalogId = request.ServiceCatalogId,
            };

            _unitOfWork.ResolveReport.Add(resolveReport);

            if (request.ImageUrl != null && request.ImageUrl.Length > 0)
            {
                byte[]? mainImageBytes = request.ImageUrl;
                using (var stream = new MemoryStream(request.ImageUrl))
                {
                    var file = new FormFile(stream, 0, stream.Length, request.FileName, request.FileName);
                    var image = await Upload.UploadFilesNames(file, _hostingEnvironment, "Groups/" + resolveReport.Id, request.FileName);
                        resolveReport.ImageUrl = image;
                }
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully(null, "Resolve Report added successfully");

        }

    }
}