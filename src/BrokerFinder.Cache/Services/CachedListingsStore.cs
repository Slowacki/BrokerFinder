using BrokerFinder.Cache.Services.Contracts;
using BrokerFinder.Core.Models;
using BrokerFinder.Core.Services.Contracts;

namespace BrokerFinder.Cache.Services;

public class CachedListingsStore(IListingsStore listingsStore, ICacheService cacheService) : IListingsStore
{
    public async Task<IEnumerable<Listing>> GetAsync(string location, ListingType type, ListingProperties properties)
    {
        var key = GenerateKey(location, type, properties);

        var cachedData = await cacheService.GetDataAsync<IEnumerable<Listing>>(key);

        if (cachedData is not null)
            return cachedData;

        var data = await listingsStore.GetAsync(location, type, properties);

        await cacheService.SetDataAsync(key, data, TimeSpan.FromMinutes(15));

        return data;
    }

    private string GenerateKey(string location, ListingType type, ListingProperties properties)
    {
        return $"{location}_{type.ToString()}_{properties.ToString()}";
    }
}