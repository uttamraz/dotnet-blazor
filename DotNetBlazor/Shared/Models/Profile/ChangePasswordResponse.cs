using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class ChangePasswordResponse
    {
        public ChangePasswordResponseData Data { get; set; }
        public Response Response { get; set; }
    }

    public class ChangePasswordResponseData
    {
        public bool IsSuccess { get; set; }
    }
}
