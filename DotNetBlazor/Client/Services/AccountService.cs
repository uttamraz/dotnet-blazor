using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using System.Net;

namespace DotNetBlazor.Client.Services
{
    public interface IAccountService : IDisposable
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        public Task<bool> IsAuthenticated();
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);

        event Action<List<Error>> ValidationError;
        event Action UnauthorizedPopup;
    }

    public class AccountService : IAccountService
    {
        private readonly IApiHelper _apiHelper;
        private readonly ICacheHelper _cacheHelper;
        public event Action<List<Error>>? ValidationError;
        public event Action UnauthorizedPopup;

        public AccountService(IApiHelper apiHelper, ICacheHelper cacheHelper)
        {
            _apiHelper = apiHelper;
            _cacheHelper = cacheHelper;
            _apiHelper.ValidationError += SetValidationError;
            _apiHelper.UnauthorizedPopup += ActionUnauthorizedPopup;
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
        private void SetValidationError(List<Error> list)
        {
            ValidationError?.Invoke(list);
        }
        private void ActionUnauthorizedPopup()
        {
            UnauthorizedPopup?.Invoke();
        }

        public void Dispose()
        {
            _apiHelper.ValidationError -= SetValidationError;
            _apiHelper.UnauthorizedPopup -= ActionUnauthorizedPopup;
        }
    }

}