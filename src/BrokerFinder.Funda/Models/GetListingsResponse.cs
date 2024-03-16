using Newtonsoft.Json;

namespace BrokerFinder.Funda.Models;

public record GetListingsResponse
{
    [JsonProperty("Objects")]
    public IEnumerable<FundaListing> Results { get; set; } = default!;
    
    [JsonProperty("Paging")]
    public Pagination Pagination { get; set; } = default!;
}