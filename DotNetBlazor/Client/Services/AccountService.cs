using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace DotNetBlazor.Client.Services
{
    public interface IAccountService
    {
        Task<Response> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task<Response> RegisterAsync(RegistrationRequest request);
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
            var responseData = new LoginResponse(null, (int)HttpStatusCode.InternalServerError, "Internal Server Error");
            var response = await _apiHelper.Post<LoginResponse>(component, "api/v1/account/login", request);

            if ((int)response.StatusCode == (int)HttpStatusCode.OK)
            {
                responseData = await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            if ((int)response.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                responseData.Response = errorData.Response;
            }
            else
            {
                var commonData = await response.Content.ReadFromJsonAsync<CommonResponse>();
                responseData.Response = commonData.Response;
            }
            return responseData;
        }

        public async Task LogoutAsync()
        {
            await _cacheHelper.RemoveTokenAsync();
        }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_cacheHelper.GetTokenAsync().Result);




        public async Task<Response> RegisterAsync(RegistrationRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/account/register", request);

            if (response.IsSuccessStatusCode)
            {
                var regnResponse = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
                return regnResponse?.Response;
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                // Handle conflict (e.g., email already exists)
            }
            else
            {
                // Handle other registration failure scenarios
            }

            return new Response { Status = (int)HttpStatusCode.InternalServerError, Message = "Internal Server Error!" };
        }
    }

}