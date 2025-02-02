namespace FAQ.Application.Extensions
{
    public static class LoggerExtension
    {
        public static void LogException(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Error, ex, ex.Message, ex != null && ex.InnerException != null ? ex.InnerException.Message : "");
        }
    }
}
