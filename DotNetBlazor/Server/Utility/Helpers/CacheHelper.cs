using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public static class CacheHelper
    {
        public static Task SetRecordAsync<T>(this IMemoryCache cache,
                                                   string recordId,
                                                   T data,
                                                   TimeSpan? absoluteExpireTime = null,
                                                   TimeSpan? slidingExpireTime = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = slidingExpireTime
            };

            var jsonData = JsonSerializer.Serialize(data);
            cache.Set(recordId, jsonData, cacheEntryOptions);
            return Task.CompletedTask;
        }

        public static Task<T?> GetRecordAsync<T>(this IMemoryCache cache,
                                                       string recordId)
        {
            if (cache.TryGetValue(recordId, out var jsonData))
            {
                return Task.FromResult(JsonSerializer.Deserialize<T>(jsonData.ToString()));
            }

            return Task.FromResult<T?>(default);
        }
    }
}
