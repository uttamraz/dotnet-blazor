using DotNetBlazor.Shared.Models.Common;
using System.Net;
using System.Net.Http.Json;
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
        public event Action<List<Error>>? ValidationError;

        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
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
            var response = await _httpClient.PostAsJsonAsync(url, data);

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
    }

}
