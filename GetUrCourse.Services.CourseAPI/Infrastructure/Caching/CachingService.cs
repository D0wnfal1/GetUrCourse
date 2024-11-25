using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Caching;

public class CachingService(IDistributedCache _cache) : ICachingService
{
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
        var jsonData = JsonConvert.SerializeObject(value);
        await _cache.SetStringAsync(key, jsonData, options);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var jsonData = await _cache.GetStringAsync(key);
        return jsonData is null ? default : JsonConvert.DeserializeObject<T>(jsonData);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}