using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Account
{
    public class LoginResponse
    {
        public LoginResponseData Data { get; set; }
        public Response Response { get; set; }
    }

    public class LoginResponseData
    {
        public string Token { get; set; }
    }
}
