using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Account
{
    public class LoginResponse
    {
        public LoginResponseData Data { get; set; }
        public Response Response { get; set; }

        public LoginResponse(LoginResponseData data, int status, string message)
        {
            Data = data;
            Response = new Response
            {
                Status = status,
                Message = message
            };
        }
    }

    public class LoginResponseData
    {
        public string Token { get; set; }
    }
}
