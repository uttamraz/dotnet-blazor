using DotNetBlazor.Shared.Models.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DotNetBlazor.Client.Utility
{

    public interface IApiHelper
    {
        event Action<List<Error>> ValidationError;
        event Action SessionExpired;
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string url, object data);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheHelper _cacheHelper;
        private readonly IEventHelper _eventHelper;

        public event Action<List<Error>>? ValidationError;
        public event Action? SessionExpired;

        public ApiHelper(HttpClient httpClient, ICacheHelper cacheHelper, IEventHelper eventHelper)
        {
            _httpClient = httpClient;
            _cacheHelper = cacheHelper;
            _eventHelper = eventHelper;
        }

        public async Task<T> Get<T>(string url)
        {
            var request = await CreateRequest(url, HttpMethod.Get);
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _eventHelper.SessionExpiredPopup();
            }
            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                if (errorData?.Data?.Errors?.Count > 0)
                {
                    _eventHelper.SetValidationError(errorData?.Data?.Errors);
                }
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default;
        }

        public async Task<T> Post<T>(string url, object data)
        {
            var request = await CreateRequest(url, HttpMethod.Post, data);
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _eventHelper.SessionExpiredPopup();
            }
            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                if (errorData?.Data?.Errors?.Count > 0)
                {
                    _eventHelper.SetValidationError(errorData?.Data?.Errors);
                }
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default;
        }


        private async Task<HttpRequestMessage> CreateRequest(string url, HttpMethod method, object data = null)
        {
            var request = new HttpRequestMessage(method, url);

            var token = await _cacheHelper.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (data != null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            }
            return request;
        }
    }

}
