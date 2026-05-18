using System.Net;
using System.Text.Json;

namespace UserManagementAPI.Midlewares
{
    public class MErrorHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MErrorHandling> _logger;

        public MErrorHandling (RequestDelegate next, ILogger<MErrorHandling> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync (HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            var statusCode = context.Response.StatusCode;

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error not mounted");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { erro = "Erro interno do servidor." };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

        }

    }
}
