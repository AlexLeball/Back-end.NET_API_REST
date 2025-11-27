using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P7CreateRestApi.Middlewares
{
    public class RequestLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogging> _logger;

        public RequestLogging(RequestDelegate next, ILogger<RequestLogging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var userName = context.User?.Identity?.IsAuthenticated == true
                ? context.User.Identity.Name
                : "Anonymous";

            var method = request.Method;
            var path = request.Path;
            var timestamp = DateTime.UtcNow;

            _logger.LogInformation("{Method} {Path} by {User} at {Timestamp}",
                method, path, userName, timestamp);

            // Continue the pipeline
            await _next(context);

            var responseStatus = context.Response.StatusCode;

            _logger.LogInformation("Response {StatusCode} for {Method} {Path} by {User} at {Timestamp}",
                responseStatus, method, path, userName, DateTime.UtcNow);
        }
    }

    // Extension method to simplify registration
    public static class RequestLoggingExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogging>();
        }
    }
}
