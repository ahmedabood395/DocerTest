
namespace _4EPlatform_FAQ.GrpClientsConfig
{
    public static class GrpcServiceClients
    {
        public static IHttpClientBuilder AddGrpcServiceClient<TService>(
            this IServiceCollection services,
            string serviceUrl)
            where TService : ClientBase
        {
            return services.AddGrpcClient<TService>(o =>
            {
                o.Address = new Uri(serviceUrl);
                o.CallOptionsActions.Add(async (callOptionsContext) =>
                {
                    var tokenService = callOptionsContext.ServiceProvider.GetRequiredService<ITokenService>();
                    var callOptions = tokenService.AcquireToken().Result;
                    callOptionsContext.CallOptions = callOptions;

                });
            });
        }
    }
}
