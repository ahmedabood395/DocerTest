namespace TicketingSystem.TicketingServices.MiddleWares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly ResponseDTO _responseDto;
        private readonly IHostEnvironment _host;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
                IHostEnvironment host)
        {
            _logger = logger;
            _responseDto = new ResponseDTO();
            _host = host;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error occurred while processing the request Message:{ex.Message}, StackTrace:{ex.StackTrace}");
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = _host.IsDevelopment() ? $"{ex?.InnerException?.Message}"

                    : $"An error occurred. Please contact the system administrator";
                var serializedResponse = JsonSerializer.Serialize(_responseDto);
                await context.Response.WriteAsync(serializedResponse);

            }
        }
    }
}
