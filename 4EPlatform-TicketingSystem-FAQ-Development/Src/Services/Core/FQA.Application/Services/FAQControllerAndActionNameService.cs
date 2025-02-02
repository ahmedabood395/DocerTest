

namespace Application.Services
{
    [Authorize("ClientCredentialsPolicy")]
    public class FAQControllerAndActionNameService : FAQControllerAndActionNameProto.FAQControllerAndActionNameProtoBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        public FAQControllerAndActionNameService(
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }



        public async override Task<GetControllerAndActionNameResponse> GetControllerAndActionName(Empty request, ServerCallContext context)
        {
            var response = new GetControllerAndActionNameResponse();

            long key = 32;
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Where(
          ad => ad.AttributeRouteInfo != null).ToList();

            foreach (var item in routes)
            {
                var ControllerInfo = new ModulaInfo()
                {

                    Key = key,
                    ControllerName = item.RouteValues["controller"],
                    Path = item.AttributeRouteInfo.Template,
                    MethodVerd = item.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First(),
                    ActionName = item.RouteValues["action"]
                };
                var controllerName = item.RouteValues["controller"];
                var actionName = item.RouteValues["action"];

                response.ModulaInfo.Add(ControllerInfo);

            }
            return response;
        }
    }
}
