namespace BrokerFinder.Core.Models;

public class Listing
{
    public string Id { get; set; } = default!;
    public int? BrokerId { get; set; }
    public string? BrokerName { get; set; }
}