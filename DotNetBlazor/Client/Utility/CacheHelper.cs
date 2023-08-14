using Microsoft.JSInterop;

namespace DotNetBlazor.Client.Utility
{
    public interface ICacheHelper
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task RemoveTokenAsync();
    }
    public class CacheHelper : ICacheHelper
    {
        private const string TokenKey = "GmpGJGEGrPxDinZGtb3KEJL5ybJhFWs0";
        private readonly IJSRuntime _jsRuntime;

        public CacheHelper(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        }

        public async Task SetTokenAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }

        public async Task RemoveTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
    }
}