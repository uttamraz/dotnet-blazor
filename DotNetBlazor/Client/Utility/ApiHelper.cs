using DotNetBlazor.Client.Services;
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
        JsonSerializerOptions option = new() { PropertyNameCaseInsensitive = true };

        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, option);
        }

        public async Task<T> Get<T>(ComponentBase component, string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if ((int)response.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                SetFormErrors(component, errorData?.Data?.Errors);
            }
            return JsonSerializer.Deserialize<T>(content, option);
        }

        public async Task<T> Post<T>(string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, option);
        }

        public async Task<T> Post<T>(ComponentBase component, string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();


            if ((int)response.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
            {
                var errorData = await response.Content.ReadFromJsonAsync<ValidationErrorResposne>();
                SetFormErrors(component, errorData?.Data?.Errors);
            }
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private void SetFormErrors(ComponentBase component, List<Error> errors)
        {
            if (component.GetType().GetProperty("editContext")?.GetValue(component) is EditContext editContext)
            {
                var messages = new ValidationMessageStore(editContext);
                foreach (var error in errors)
                {
                    messages.Add(editContext.Field(error.Field), error.Message);
                }
                editContext.NotifyValidationStateChanged();
            }
        }
    }

}
