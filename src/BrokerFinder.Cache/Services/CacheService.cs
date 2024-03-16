using BrokerFinder.Cache.Services.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BrokerFinder.Cache.Services;

public class CacheService(IDatabase database) : ICacheService 
{
    public async Task<T?> GetDataAsync<T>(string key) 
    {
        var value = await database.StringGetAsync(key);
        
        if (!string.IsNullOrEmpty(value)) 
        {
            return JsonConvert.DeserializeObject<T>(value!);
        }
        
        return default;
    }
    
    public async Task<bool> SetDataAsync<T>(string key, T value, TimeSpan? expireIn) 
    {
        var isSet = await database.StringSetAsync(key, JsonConvert.SerializeObject(value), expireIn);
        
        return isSet;
    }
    
    public async Task<bool> RemoveDataAsync(string key) 
    {
        var keyExists = await database.KeyExistsAsync(key);
        
        if (keyExists) 
        {
            return await database.KeyDeleteAsync(key);
        }
        
        return false;
    }
}