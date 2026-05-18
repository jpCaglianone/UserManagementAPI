namespace UserManagementAPI.Midlewares
{
    public class MTokenValidation
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MTokenValidation> _logger;

        public MTokenValidation(RequestDelegate next, ILogger<MTokenValidation> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (path != null && path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            var _tokken = context.Request.Headers["Authorization"].ToString();

            _logger.LogInformation("Received request with token: {Token}", _tokken);

            string _secret = "mysecrettoken";

            if (!context.Request.Headers.TryGetValue("Authorization", out var token) || token != "Bearer " + _secret)
            {
                _logger.LogWarning("Unauthorized access attempt with token: {Token}", token);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await _next(context);
        }
    }
}
