using DotNetBlazor.Shared.Models.Account;

namespace DotNetBlazor.Server.Services.AccountService
{
    public interface IAccountService
    {
        Task<RegistrationResponseData> RegisterUser(RegistrationRequest request);
        Task<LoginResponseData> Login(LoginRequest request);
    }
}
