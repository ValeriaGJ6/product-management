using System.Net;

namespace ProductManagement.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Asigna el status code basado en el tipo de excepción
            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                FluentValidation.ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var errorDetails = new
            {
                title = statusCode == (int)HttpStatusCode.InternalServerError
                    ? "Internal Server Error."
                    : "One or more validation errors occurred.",
                status = context.Response.StatusCode,
                message = statusCode == (int)HttpStatusCode.InternalServerError
                    ? "An unexpected error occurred. Please try again."
                    : exception.Message,
                detail = _env.IsDevelopment() ? exception.StackTrace : null
            };

            await context.Response.WriteAsJsonAsync(errorDetails);
        }
    }
}
