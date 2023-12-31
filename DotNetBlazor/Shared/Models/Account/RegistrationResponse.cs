using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Account
{
    public class RegistrationResponse
    {
        public RegistrationResponseData Data { get; set; }
        public Response Response { get; set; }

    }

    public class RegistrationResponseData
    {
        public bool IsSuccess { get; set; }
    }
}