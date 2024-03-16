﻿using BrokerFinder.Core.Models;
using BrokerFinder.Core.Services.Contracts;

namespace BrokerFinder.Core.Services;

public class BrokersService(IListingsStore listingsStore) : IBrokersService
{
    public async Task<IEnumerable<Broker>> GetTopByListingsCountAsync(string listingLocation, ListingType listingType, ListingProperties listingProperties, int limit = 10)
    {
        var listings = await listingsStore.GetAsync(listingLocation, listingType, listingProperties);

        var brokers = listings
            .Where(l => l.BrokerId is not null)
            .GroupBy(l => l.BrokerId)
            .Select(g => new Broker
            {
                Id = g.Key!.Value,
                Name = g.First().BrokerName!,
                NumberOfListings = g.Count()
            })
            .OrderByDescending(b => b.NumberOfListings)
            .Take(limit);

        return brokers;
    }
}