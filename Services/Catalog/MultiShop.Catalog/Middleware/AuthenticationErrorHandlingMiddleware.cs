using System.Net;
using System.Text.Json;

namespace MultiShop.Catalog.Middleware
{
    public class AuthenticationErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationErrorHandlingMiddleware> _logger;

        public AuthenticationErrorHandlingMiddleware(RequestDelegate next, ILogger<AuthenticationErrorHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An error occurred in the request pipeline");
                await HandleExceptionAsync(context, ex);
                return;
            }

            // Handle authentication/authorization responses
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                await HandleUnauthorizedAsync(context);
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                await HandleForbiddenAsync(context);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new
            {
                error = "An error occurred while processing your request",
                message = exception.Message,
                timestamp = DateTime.UtcNow
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(jsonResponse);
        }

        private async Task HandleUnauthorizedAsync(HttpContext context)
        {
            if (!context.Response.HasStarted)
            {
                var response = new
                {
                    error = "Unauthorized",
                    message = "Authentication required. Please provide a valid JWT token.",
                    timestamp = DateTime.UtcNow
                };

                var jsonResponse = JsonSerializer.Serialize(response);
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsync(jsonResponse);
            }
        }

        private async Task HandleForbiddenAsync(HttpContext context)
        {
            if (!context.Response.HasStarted)
            {
                var response = new
                {
                    error = "Forbidden",
                    message = "Insufficient permissions. You don't have access to this resource.",
                    timestamp = DateTime.UtcNow
                };

                var jsonResponse = JsonSerializer.Serialize(response);
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}