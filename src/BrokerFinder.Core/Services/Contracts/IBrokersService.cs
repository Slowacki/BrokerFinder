using BrokerFinder.Core.Models;

namespace BrokerFinder.Core.Services.Contracts;

public interface IBrokersService
{
    Task<IEnumerable<Broker>> GetTopByListingsCountAsync(string listingLocation, ListingType listingType, ListingProperties listingProperties, int limit = 10);
}