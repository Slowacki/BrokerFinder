using BrokerFinder.Core.Models;
using BrokerFinder.Core.Services.Contracts;
using BrokerFinder.Funda.Apis;
using BrokerFinder.Funda.Models;

namespace BrokerFinder.Funda.Services;

public class FundaListingsStore(IListingsApi listingsApi) : IListingsStore
{
    public async Task<IEnumerable<Listing>> GetAsync(string location, ListingType type, ListingProperties properties, CancellationToken cancellationToken = default)
    {
        var query = $"/{location}";

        if (properties.HasFlag(ListingProperties.HasGarden))
        {
            query += "/tuin";
        }

        var listingType = type == ListingType.Buy ? "koop" : "huur";
        var fundaListingsResponse = await listingsApi.GetListingsAsync(listingType, query, 1);
        var fundaListings = fundaListingsResponse.Results.ToList();
        
        while (fundaListingsResponse.Pagination.CurrentPage < fundaListingsResponse.Pagination.PagesCount)
        {
            fundaListingsResponse = await listingsApi.GetListingsAsync(listingType, query, fundaListingsResponse.Pagination.CurrentPage + 1);
            fundaListings.AddRange(fundaListingsResponse.Results);
        }

        return fundaListings.Select(MapToListing);
    }

    private Listing MapToListing(FundaListing listing)
    {
        return new Listing(listing.Id, listing.BrokerId, listing.BrokerName);
    }
}