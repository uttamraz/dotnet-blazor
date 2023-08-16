namespace DotNetBlazor.Shared.Models.Common
{
    public class CommonResponse
    {
        public object Data { get; set; }
        public Response Response { get; set; }

        public CommonResponse(object data, int status, string message)
        {
            Data = data;
            Response = new Response
            {
                Status = status,
                Message = message
            };
        }
    }

    public class ValidationErrorResposne
    {
        public ErrorList Data { get; set; }
        public Response Response { get; set; }
    }

    public class ErrorList
    {
        public List<Error>? Errors { get; set; }
    }
}
