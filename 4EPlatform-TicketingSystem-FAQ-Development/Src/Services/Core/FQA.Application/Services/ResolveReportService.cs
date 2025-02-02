
namespace _4EPlatform_Application.Services
{
    public class ResolveReportService : FAQService.FAQServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _responseDto;
        private readonly IMediator _mediator;

        public ResolveReportService(IUnitOfWork unitOfWork, IHttpContextAccessor _httpContextAccessor, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDTO();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        public async override Task<GetResolveByIdResponse> GetResolveById(GetResolveByIdRequester request, ServerCallContext context)
        {
            var response = new GetResolveByIdResponse();

            try
            {
                var entityObj = _unitOfWork.ResolveReport.GetAll(x => x.Id == new Guid(request.Id) && x.State != State.Deleted).FirstOrDefault();

                if (entityObj != null)
                {
                    response.ImageUrl = entityObj.ImageUrl;
                    response.ResolveDescription = entityObj.ResolveDescription;
                    response.ResolveName = entityObj.ResolveName;
                    response.CreatedBy = entityObj.CreatedBy.ToString();
                    response.CreatedOn = entityObj.CreatedOn.ToString("yyyy/MM/dd");
                    response.UpdatedOn = entityObj.UpdatedOn.HasValue ? entityObj.UpdatedOn?.ToString("yyyy/MM/dd") : "";
                    response.TimeOfCreation = entityObj.CreatedOn.ToString("T");
                    response.MainClassificationId = entityObj.MainClassificationId.ToString();
                    response.ServiceCatalogId = entityObj.ServiceCatalogId.ToString();

                    return response;
                }
                return null;

            }
            catch (Exception)
            {
            }
            return response;
        }
        public async override Task<GetAllResolveResponse> GetAllResolve(GetAllResolveRequester request, ServerCallContext context)
        {
            try
            {
                var response = new GetAllResolveResponse();
                var entityObj = new List<GetAllResolveDTO>();
                if (request.MainClassificationId == null || Guid.Parse(request.MainClassificationId)==Guid.Empty)
                {
                    entityObj = _unitOfWork.ResolveReport.GetAll(x => request.Name == "" || x.ResolveName.Contains(request.Name), int.Parse(request.PageNumber), int.Parse(request.PageSize), ref _responseDto,n=>n.CreatedOn).Select(x => new GetAllResolveDTO
                    {
                        Id = x.Id.ToString(),
                        ResolveName = x.ResolveName,
                        ResolveDescription = x.ResolveDescription,
                        CreatedOn = x.CreatedOn.ToString(),
                        MainClassificationId = x.MainClassificationId.ToString()
                    }).ToList();
                }
                else
                {
                    entityObj = _unitOfWork.ResolveReport.GetAll(x => x.MainClassificationId.ToString() == request.MainClassificationId, int.Parse(request.PageNumber), int.Parse(request.PageSize), ref _responseDto, n => n.CreatedOn).Select(x => new GetAllResolveDTO
                    {
                        Id = x.Id.ToString(),
                        ResolveName = x.ResolveName,
                        ResolveDescription = x.ResolveDescription,
                        CreatedOn = x.CreatedOn.ToString(),
                        MainClassificationId = x.MainClassificationId.ToString()
                    }).ToList();
                }
           

                if (entityObj != null)
                {
                    response.ResolveDTO.AddRange(entityObj);
                    response.PageIndex = _responseDto.PageIndex;
                    response.PageSize = _responseDto.PageSize;
                    response.TotalPages = _responseDto.TotalPages;
                    response.TotalItems = _responseDto.TotalItems;

                    return response;
                }

            }
            catch (Exception)
            {
            }
            return null;
        }
        public async override Task<PostResolveResponse> PostResolve(PostResolveRequester request, ServerCallContext context)
        {
            byte[]? mainImageBytes = request.ImageUrl?.ToByteArray();
            var result = await _mediator.Send(new PostResolveReportCommand
            {
                ResolveDescription = request.ResolveDescription,
                ResolveName = request.ResolveName,
                MainClassificationId = new Guid(request.MainClassificationId),
                ImageUrl = mainImageBytes,
                ServiceCatalogId = new Guid(request.ServiceCatalogId),
                FileName = request.FileName
            });

            PostResolveResponse postResolveResponse = new PostResolveResponse();
            postResolveResponse.Message = result.Message;

            return postResolveResponse;
        }
        public async override Task<PutResolveResponse> PutResolve(PutResolveRequester request, ServerCallContext context)
        {
            byte[]? mainImageBytes = request.ImageUrl?.ToByteArray();

            var result = await _mediator.Send(new PutResolveReportCommand
            {
                ResolveDescription = request.ResolveDescription,
                ResolveName = request.ResolveName,
                MainClassificationId = new Guid(request.MainClassificationId),
                ImageUrl = mainImageBytes,
                ServiceCatalogId = new Guid(request.ServiceCatalogId),
                Id = new Guid(request.Id),
                FileName = request.FileName,
                AttachmentUrl = request.AttachmentUrl
            });

            PutResolveResponse putResolveResponse = new PutResolveResponse();
            putResolveResponse.Message = result.Message;

            return putResolveResponse;
        }
    }
}

