namespace BrokerFinder.Cache.Services.Contracts;

public interface ICacheService
{
    /// <summary>
    /// Get cached data if exists
    /// </summary>
    /// <typeparam name="T">Type to deserialize the data to</typeparam>
    /// <param name="key">Key to look for</param>
    /// <returns>Deserialized data if exists or null otherwise</returns>
    Task<T?> GetDataAsync<T>(string key);

    /// <summary>
    /// Serialize and create a cache entry
    /// </summary>
    /// <typeparam name="T">Type of the data to be serialized</typeparam>
    /// <param name="key">The key of the cache entry</param>
    /// <param name="value">Data to be serialized</param>
    /// <param name="expireIn">Optional lifespan of the cache entry</param>
    /// <returns>true if cache entry was created successfully, false otherwise</returns>
    Task<bool> SetDataAsync<T>(string key, T value, TimeSpan? expireIn);

    /// <summary>
    /// Remove cached entry
    /// </summary>
    /// <param name="key">The key of the entry to be removed</param>
    /// <returns>True if key existed and was successfully removed, false otherwise</returns>
    Task<bool> RemoveDataAsync(string key);
}