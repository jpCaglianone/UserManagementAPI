namespace UserManagementAPI.Midlewares
{
    public class MReqRespLog
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MReqRespLog>  _logger;

        public MReqRespLog(RequestDelegate next, ILogger<MReqRespLog> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            var method = context.Request.Method;
            var path = context.Request.Path;
           
            await _next(context);

            var statusCode = context.Response.StatusCode;

            _logger.LogInformation(
           "HTTP {Method} {Path} response with status code {StatusCode}",
           method,
           path,
           statusCode
           );
        }

    }
}
