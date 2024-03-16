using BrokerFinder.Core.Models;

namespace BrokerFinder.Core.Services.Contracts;

public interface IBrokersService
{
    /// <summary>
    /// Get brokers offering most listings of a specific type and properties in a given location
    /// </summary>
    /// <param name="listingLocation">Location of the listings</param>
    /// <param name="listingType">Type of the listing (buy/rent)</param>
    /// <param name="listingProperties">Additional properties of the listing</param>
    /// <param name="limit">Number of results to be returned</param>
    /// <returns>List of brokers sorted descendingly by the number of available listings per broker</returns>
    Task<IEnumerable<Broker>> GetTopByListingsCountAsync(string listingLocation, ListingType listingType, ListingProperties listingProperties, int limit = 10);
}