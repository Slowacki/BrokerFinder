using AutoFixture;
using BrokerFinder.Core.Models;
using BrokerFinder.Core.Services;
using BrokerFinder.Core.Services.Contracts;
using Moq;

namespace BrokerFinder.Core.Tests.Services;

public class BrokersServiceTests
{
    private readonly BrokersService _brokersService;
    private readonly Mock<IListingsStore> _listingsStoreMock = new();
    private readonly Fixture _fixture = new();

    private readonly int _brokerId1;
    private readonly int _brokerId2;
    private readonly int _brokerId3;
    
    public BrokersServiceTests()
    {
        _brokersService = new(_listingsStoreMock.Object);
        
        _brokerId1 = _fixture.Create<int>();
        _brokerId2 = _fixture.Create<int>();
        _brokerId3 = _fixture.Create<int>();
    }

    [Fact(DisplayName = "GetTopByListingsCountAsync returns only the top brokers when limit is provided")]
    public async Task GetTopByListingsCountAsync_ReturnsOnlyTopResults_WhenLimitIsProvided()
    {
        var limit = 2;

        _listingsStoreMock
            .Setup(e => e.GetAsync(
                It.IsAny<string>(),
                It.IsAny<ListingType>(),
                It.IsAny<ListingProperties>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(GetListingsSample());


        var results = (await _brokersService.GetTopByListingsCountAsync(_fixture.Create<string>(),
            _fixture.Create<ListingType>(), 
            _fixture.Create<ListingProperties>(), 
            limit, 
            CancellationToken.None))
            .ToList();

        Assert.NotEmpty(results);
        Assert.All(results, r => Assert.True(r.Id == _brokerId1 || r.Id == _brokerId2));
    }
    
    [Fact(DisplayName = "GetTopByListingsCountAsync returns brokers ordered descendingly by the number of listings")]
    public async Task GetTopByListingsCountAsync_ReturnsBrokersOrderedDescendinglyByNumberOfListings()
    {
        var limit = 5;

        _listingsStoreMock
            .Setup(e => e.GetAsync(
                It.IsAny<string>(),
                It.IsAny<ListingType>(),
                It.IsAny<ListingProperties>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(GetListingsSample());

        var results = (await _brokersService.GetTopByListingsCountAsync(_fixture.Create<string>(),
                _fixture.Create<ListingType>(), 
                _fixture.Create<ListingProperties>(), 
                limit, 
                CancellationToken.None))
            .ToList();

        Assert.NotEmpty(results);
        Assert.Equal(_brokerId2, results.First().Id);
        Assert.Equal(_brokerId1, results.Skip(1).First().Id);
        Assert.Equal(_brokerId3, results.Skip(2).First().Id);
    }

    private IEnumerable<Listing> GetListingsSample()
    {
        return
        [
            _fixture.Build<Listing>().With(p => p.BrokerId, _brokerId1).Create(),
            _fixture.Build<Listing>().With(p => p.BrokerId, _brokerId1).Create(),
            _fixture.Build<Listing>().With(p => p.BrokerId, _brokerId2).Create(),
            _fixture.Build<Listing>().With(p => p.BrokerId, _brokerId2).Create(),
            _fixture.Build<Listing>().With(p => p.BrokerId, _brokerId2).Create(),
            _fixture.Build<Listing>().With(p => p.BrokerId, _brokerId3).Create()
        ];
    }
}