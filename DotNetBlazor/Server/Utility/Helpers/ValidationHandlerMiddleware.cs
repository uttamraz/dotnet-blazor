using DotNetBlazor.Shared.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Text.Json;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public class ValidationHandlerMiddleware : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
    public class ValidationResultModel
    {
        public ErrorData Data { get; }
        public Response Response { get; }
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Data = new ErrorData(modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(
                        x => new ValidationError(
                            JsonNamingPolicy.CamelCase.ConvertName(key), x.ErrorMessage
                            )
                        ))
                    .ToList());
            Response = new Response()
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Message = "Validation Error!"
            };
        }
    }

    public class ErrorData
    {
        public List<ValidationError> Errors { get; }
        public ErrorData(List<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}