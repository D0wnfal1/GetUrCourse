namespace GetUrCourse.Services.UserAPI.Infrastructure.Caching;

public interface ICachingService
{
    Task SetAsync<T>(string key, T value, TimeSpan? expiration);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}