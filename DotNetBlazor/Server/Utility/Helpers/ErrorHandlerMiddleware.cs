using DotNetBlazor.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text.Json;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;

                string? message;
                List<ValidationError>? errorList = new();
                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        message = e?.Message;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = StatusCodes.Status404NotFound;
                        message = e?.Message;
                        break;

                    case ValidationException e:
                        // not found error
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        message = e?.Message;
                        break;

                    case UnauthorizedAccessException e:
                        // not found error
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        message = e?.Message;
                        break;
                    case CustomValidationException e:
                        response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                        message = e?.Message;
                        errorList = e?.ValidationErrors;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        message = "Internal Server Error";
                        break;
                }
                var result = new CommonResponse(
                    new
                    {
                        Message = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production"
                    || response.StatusCode != StatusCodes.Status500InternalServerError ? error?.Message : "Internal Server Error"
                    },
                    new Response() { Status = response.StatusCode, Message = message }
                    );

                if (response.StatusCode == StatusCodes.Status422UnprocessableEntity)
                {
                    result.Data = new ErrorData(errorList);
                }
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                await response.WriteAsync(JsonSerializer.Serialize(result, options));
            }
        }
    }
}