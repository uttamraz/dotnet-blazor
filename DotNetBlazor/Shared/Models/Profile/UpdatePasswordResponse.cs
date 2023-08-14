using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class UpdatePasswordResponse
    {
        public UpdatePasswordResponseData Data { get; set; }
        public Response Response { get; set; }

        public UpdatePasswordResponse(UpdatePasswordResponseData data, int status, string message)
        {
            Data = data;
            Response = new Response
            {
                Status = status,
                Message = message
            };
        }
    }

    public class UpdatePasswordResponseData
    {
        public bool IsSuccess { get; set; }
    }
}
