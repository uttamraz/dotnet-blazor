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
        Task<T> Get<T>(string url, object? query = null);
        Task<T> Post<T>(string url, object data);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheHelper _cacheHelper;
        private readonly IEventHelper _eventHelper;

        public ApiHelper(HttpClient httpClient, ICacheHelper cacheHelper, IEventHelper eventHelper)
        {
            _httpClient = httpClient;
            _cacheHelper = cacheHelper;
            _eventHelper = eventHelper;
        }

        public async Task<T> Get<T>(string url, object? query = null)
        {
            string queryString = QueryStringFromObject(query);
            string requestUrl = $"{url}?{queryString}";
            return await Send<T>(requestUrl, HttpMethod.Get);
        }

        public async Task<T> Post<T>(string url, object data)
        {
            return await Send<T>(url, HttpMethod.Post, data);
        }

        private async Task<T> Send<T>(string url, HttpMethod method, object? data = null)
        {
            try
            {
                _eventHelper.SetLoadingState(true);
                var request = await CreateRequest(url, method, data);
                var response = await _httpClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _cacheHelper.RemoveTokenAsync();
                    _eventHelper.HandleSessionExpire();
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _eventHelper.SetLoadingState(false);
            }
        }

        private async Task<HttpRequestMessage> CreateRequest(string url, HttpMethod method, object? data = null)
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

        private string QueryStringFromObject(object? obj)
        {
            if (obj == null) return string.Empty;
            var queryParams = new StringBuilder();
            foreach (var property in obj.GetType().GetProperties())
            {
                var value = property.GetValue(obj);
                if (value != null)
                {
                    if (queryParams.Length > 0) queryParams.Append("&");
                    queryParams.Append($"{property.Name}={Uri.EscapeDataString(value.ToString())}");
                }
            }
            return queryParams.ToString();
        }
    }

}
