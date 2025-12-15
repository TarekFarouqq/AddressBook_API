using System.Net;
using System.Text.Json;

namespace AddressBook_API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled Exception");

                context.Response.StatusCode = ex switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    InvalidOperationException => (int)HttpStatusCode.Conflict, 
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    success = false,
                    message = ex.Message,
                    error = ex.GetType().Name,
                    statusCode = context.Response.StatusCode
                };

                var json = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
