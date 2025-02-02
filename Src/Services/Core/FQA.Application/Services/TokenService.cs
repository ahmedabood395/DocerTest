namespace _4EPlatform_FAQ.Services
{
    public class TokenService : ITokenService
    {
        private IHttpContextAccessor _contextAccessor;
        private IConfiguration _configuration;

        public TokenService(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public async Task<CallOptions> AcquireToken()
        {
            string jwtToken = _contextAccessor.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return new CallOptions();
            }

            if (jwtToken.StartsWith("Bearer "))
            {
                jwtToken = jwtToken.Substring("Bearer ".Length);
            }
            var metadata = new Metadata
            {
               { "Authorization", $"Bearer {jwtToken}" }
            };
            var callOptions = new CallOptions(metadata);
            return await Task.FromResult(callOptions);
        }

    }

}
