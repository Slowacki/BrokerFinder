using BrokerFinder.Core.Models;

namespace BrokerFinder.Core.Services.Contracts;

public interface IListingsStore
{
    /// <summary>
    /// Get available listings filtered by location, type and additional properties
    /// </summary>
    /// <param name="location">Location of the listing</param>
    /// <param name="type">Type of the listing (buy/rent)</param>
    /// <param name="properties">Additional properties of the listing</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Filtered collection of listings</returns>
    Task<IEnumerable<Listing>> GetAsync(string location, ListingType type, ListingProperties properties, CancellationToken cancellationToken = default);
}