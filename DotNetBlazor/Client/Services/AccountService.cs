using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using System.Net;

namespace DotNetBlazor.Client.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        public Task<bool> IsAuthenticated();
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }

    public class AccountService : IAccountService
    {
        private readonly IApiHelper _apiHelper;
        private readonly ICacheHelper _cacheHelper;

        public AccountService(IApiHelper apiHelper, ICacheHelper cacheHelper)
        {
            _apiHelper = apiHelper;
            _cacheHelper = cacheHelper;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = await _apiHelper.Post<LoginResponse>("api/v1/account/login", request);
            if (response?.Response?.Status == (int)HttpStatusCode.OK && !string.IsNullOrEmpty(response?.Data?.Token))
            {
                await _cacheHelper.SetTokenAsync(response?.Data?.Token);
            }
            return response;
        }

        public async Task LogoutAsync()
        {
            await _cacheHelper.RemoveTokenAsync();
        }
        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            return await _apiHelper.Post<RegistrationResponse>("api/v1/account/register", request);
        }

        public async Task<bool> IsAuthenticated()
        {
            return !string.IsNullOrWhiteSpace(await _cacheHelper.GetTokenAsync());
        }
    }

}