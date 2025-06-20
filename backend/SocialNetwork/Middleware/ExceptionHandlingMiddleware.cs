using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FilmMatch.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Произошла непредвиденная ошибка.";

            switch (exception)
            {
                case ValidationException validationEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = validationEx.Message;
                    break;
                case AuthenticationException authEx:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = authEx.Message;
                    break;
                case InvalidOperationException invalidOpEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = invalidOpEx.Message;
                    break;
                case ArgumentException argEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = argEx.Message;
                    break;
                // Можно добавить другие кастомные ошибки
                default:
                    logger.LogError(exception, "Unhandled exception");
                    break;
            }

            context.Response.StatusCode = statusCode;
            var result = JsonSerializer.Serialize(new { error = message });
            await context.Response.WriteAsync(result);
        }
    }
} 