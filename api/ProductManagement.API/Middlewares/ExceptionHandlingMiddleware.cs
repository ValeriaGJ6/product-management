using System.Net;

namespace ProductManagement.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        private readonly Dictionary<Type, int> _exceptionStatusCodes = new()
        {
            { typeof(KeyNotFoundException), (int)HttpStatusCode.NotFound },
            { typeof(FluentValidation.ValidationException), (int)HttpStatusCode.BadRequest },
            { typeof(ArgumentException), (int)HttpStatusCode.BadRequest },
            { typeof(ArgumentNullException), (int)HttpStatusCode.BadRequest },
            { typeof(InvalidOperationException), (int)HttpStatusCode.BadRequest },
            { typeof(UnauthorizedAccessException), (int)HttpStatusCode.Unauthorized },
            { typeof(NotImplementedException), (int)HttpStatusCode.NotImplemented }
        };

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
                _logger.LogError(ex, "Ha ocurrido una excepción no controlada.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = GetStatusCode(exception);
            context.Response.StatusCode = statusCode;

            var errorDetails = new
            {
                title = GetTitle(statusCode),
                status = statusCode,
                message = GetMessage(exception, statusCode),
                detail = _env.IsDevelopment() ? exception.StackTrace : null,
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsJsonAsync(errorDetails);
        }

        private int GetStatusCode(Exception exception)
        {
            if (_exceptionStatusCodes.TryGetValue(exception.GetType(), out var statusCode))
                return statusCode;

            return (int)HttpStatusCode.InternalServerError;
        }

        private static string GetTitle(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            500 => "Internal Server Error",
            _ => "Error"
        };

        private static string GetMessage(Exception exception, int statusCode)
        {
            return statusCode == (int)HttpStatusCode.InternalServerError
                ? "Ha ocurrido un error inesperado. Por favor, inténtelo de nuevo."
                : exception.Message;
        }
    }
}
