using System.Diagnostics;

namespace FunBooksAndVideos.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            _logger.LogInformation("Incoming Request: {Method} {Path}", request.Method, request.Path);

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation("Completed Request: {Method} {Path} - {StatusCode} ({ElapsedMilliseconds}ms)",
                request.Method, request.Path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
    }
}
