using BrokerFinder.Core.Models;

namespace BrokerFinder.Core.Services.Contracts;

public interface IListingsStore
{
    Task<IEnumerable<Listing>> GetAsync(string location, ListingType type, ListingProperties properties);
}