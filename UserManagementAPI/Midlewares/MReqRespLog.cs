namespace UserManagementAPI.Midlewares
{
    public class MReqRespLog
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MReqRespLog> _logger;

        public MReqRespLog(RequestDelegate next, ILogger<MReqRespLog> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;
            var start = DateTime.UtcNow;

            _logger.LogInformation("Incoming request: {Method} {Path}", method, path);

            await _next(context);

            var elapsedMs = (DateTime.UtcNow - start).TotalMilliseconds;

            _logger.LogInformation(
                "Outgoing response: {Method} {Path} {StatusCode} in {ElapsedMs}ms",
                method,
                path,
                context.Response.StatusCode,
                elapsedMs
            );
        }
    }
}