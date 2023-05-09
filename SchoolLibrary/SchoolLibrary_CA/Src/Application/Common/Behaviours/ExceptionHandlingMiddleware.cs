using System.Text.Json;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }


        // Request handling method
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // If everything goes well, the _next delegate should process the request
                await next(context);
                
                // But if a request is unsuccessful, middleware will trigger
                // the catch block and call the HandleExceptionAsync method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        // Sets up the response status code and content type and return a response
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static int GetStatusCode(Exception exception)
        {
            if (exception is NotFoundException) return StatusCodes.Status404NotFound;
            if (exception is BadRequestException) return StatusCodes.Status400BadRequest;
            if (exception is ValidationException) return StatusCodes.Status422UnprocessableEntity;
            return StatusCodes.Status500InternalServerError;
        }
        private static string GetTitle(Exception exception)
        {
            return exception.GetType().Name;
        }
        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = default!;

            if (exception is ValidationException validationException)
                errors = validationException.Failures.AsReadOnly();

            return errors;
        }
    }
}