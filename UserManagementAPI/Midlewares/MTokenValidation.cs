namespace UserManagementAPI.Midlewares
{
    public class MTokenValidation
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MTokenValidation> _logger;
        private readonly IConfiguration _configuration;

        public MTokenValidation(
            RequestDelegate next,
            ILogger<MTokenValidation> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";

            if (path.StartsWith("/swagger") || path.StartsWith("/openapi"))
            {
                await _next(context);
                return;
            }

            var expectedToken = _configuration["ApiToken"] ?? "mysecrettoken";

            var hasToken = context.Request.Headers.TryGetValue("Authorization", out var authHeader);
            var isValidToken = hasToken && authHeader.ToString() == $"Bearer {expectedToken}";

            if (!isValidToken)
            {
                _logger.LogWarning(
                    "Unauthorized request: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path
                );

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("""
                {
                    "status": 401,
                    "error": "Unauthorized",
                    "message": "A valid bearer token is required."
                }
                """);

                return;
            }

            await _next(context);
        }
    }
}