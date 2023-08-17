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
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string url, object data);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheHelper _cacheHelper;
        public event Action<List<Error>>? ValidationError;

        public ApiHelper(HttpClient httpClient, ICacheHelper cacheHelper)
        {
            _httpClient = httpClient;
            _cacheHelper = cacheHelper;
        }

        public async Task<T> Get<T>(string url)
        {
            var request = await CreateRequestAsync(url, HttpMethod.Get);
            var response = await _httpClient.SendAsync(request);

            if ((int)response.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                if (errorData?.Data?.Errors?.Count > 0)
                {
                    ValidationError?.Invoke(errorData?.Data?.Errors);
                }
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default(T);
        }

        public async Task<T> Post<T>(string url, object data)
        {
            var request = await CreateRequestAsync(url, HttpMethod.Post, data);
            var response = await _httpClient.SendAsync(request);

            if ((int)response.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                if (errorData?.Data?.Errors?.Count > 0)
                {
                    ValidationError?.Invoke(errorData?.Data?.Errors);
                }
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default(T);
        }


        private async Task<HttpRequestMessage> CreateRequestAsync(string url, HttpMethod method, object data = null)
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
