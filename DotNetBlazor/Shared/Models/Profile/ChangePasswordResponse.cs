using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class ChangePasswordResponse
    {
        public ChangePasswordResponseData Data { get; set; }
        public Response Response { get; set; }

        public ChangePasswordResponse(ChangePasswordResponseData data, int status, string message)
        {
            Data = data;
            Response = new Response
            {
                Status = status,
                Message = message
            };
        }
    }

    public class ChangePasswordResponseData
    {
        public bool IsSuccess { get; set; }
    }
}
