using Newtonsoft.Json;

namespace BrokerFinder.Funda.Models;

public record FundaListing
{
    public string Id { get; set; } = default!;
    
    [JsonProperty("MakelaarId")]
    public int? BrokerId { get; set; }
    
    [JsonProperty("MakelaarNaam")]
    public string? BrokerName { get; set; }
}