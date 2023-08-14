using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace DotNetBlazor.Client.Utility
{

    public interface IApiHelper
    {
        Task<T> Get<T>(string url);
        Task<T> Get<T>(ComponentBase component, string url);
        Task<T> Post<T>(string url, object data);
        Task<T> Post<T>(ComponentBase component, string url, object data);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;

        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T> Get<T>(ComponentBase component, string url)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Post<T>(string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T> Post<T>(ComponentBase component, string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if ((int)response.StatusCode != (int)HttpStatusCode.OK)
            {
                if ((int)response.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
                {
                    var errorData = JsonSerializer.Deserialize<ValidationErrorResposne>(content, option);
                    responseData.Response = errorData.Response;
                }
                else
                {
                    var commonData = JsonSerializer.Deserialize<CommonResponse>(content, option);
                    responseData.Response = commonData.Response;
                }
            }
            return JsonSerializer.Deserialize<T>(content, option);
        }

        private void SetFormErrors(ComponentBase component, List<Error> errors)
        {
            var editContext = component.GetType().GetProperty("editContext")?.GetValue(component) as EditContext;

            if (editContext != null)
            {
                foreach (var error in errors)
                {
                    editContext.Err;
                }
                editContext.NotifyValidationStateChanged();
            }
        }
    }

}
