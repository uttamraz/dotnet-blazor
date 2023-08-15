using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace DotNetBlazor.Client.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(ComponentBase component, LoginRequest request);
        Task LogoutAsync();
        public bool IsAuthenticated();
        Task<RegistrationResponse> RegisterAsync(ComponentBase component, RegistrationRequest request);
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

        public async Task<LoginResponse> LoginAsync(ComponentBase component, LoginRequest request)
        {
            var response = await _apiHelper.Post<LoginResponse>(component, "api/v1/account/login", request);
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
        public async Task<RegistrationResponse> RegisterAsync(ComponentBase component, RegistrationRequest request)
        {
            return await _apiHelper.Post<RegistrationResponse>(component, "api/v1/account/register", request);
        }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrWhiteSpace(_cacheHelper.GetTokenAsync().Result);
        }
    }

}