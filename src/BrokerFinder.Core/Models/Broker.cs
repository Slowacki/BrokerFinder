namespace BrokerFinder.Core.Models;

public class Broker
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int NumberOfListings { get; set; }
}