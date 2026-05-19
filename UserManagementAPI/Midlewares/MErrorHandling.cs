using System.Net;
using System.Text.Json;

namespace UserManagementAPI.Midlewares
{
    public class MErrorHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MErrorHandling> _logger;

        public MErrorHandling(RequestDelegate next, ILogger<MErrorHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled error on {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path
                );

                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        status = 500,
                        error = "Internal Server Error",
                        message = "An unexpected error occurred.",
                        traceId = context.TraceIdentifier
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            }
        }
    }
}