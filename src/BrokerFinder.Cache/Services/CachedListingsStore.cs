using BrokerFinder.Cache.Helpers;
using BrokerFinder.Core.Models;
using BrokerFinder.Core.Services.Contracts;
using Microsoft.Extensions.Caching.Distributed;

namespace BrokerFinder.Cache.Services;

public class CachedListingsStore(IListingsStore listingsStore, IDistributedCache cache) : IListingsStore
{
    public async Task<IEnumerable<Listing>> GetAsync(string location, ListingType type, ListingProperties properties, CancellationToken cancellationToken = default)
    {
        var key = GenerateKey(location, type, properties);

        var cachedData = await cache.GetAsync(key, cancellationToken);
        
        if (cachedData is not null)
            return SerializationHelper.Deserialize<IEnumerable<Listing>>(cachedData) ?? Array.Empty<Listing>();

        var data = await listingsStore.GetAsync(location, type, properties, cancellationToken);

        await cache.SetAsync(
            key,
            SerializationHelper.Serialize(data),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            },
            cancellationToken);

        return data;
    }

    private string GenerateKey(string location, ListingType type, ListingProperties properties)
    {
        return $"{location}_{type.ToString()}_{properties.ToString()}";
    }
}