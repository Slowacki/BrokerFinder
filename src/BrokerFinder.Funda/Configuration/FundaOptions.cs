using System.ComponentModel.DataAnnotations;

namespace BrokerFinder.Funda.Configuration;

public class FundaOptions
{
    [Required]
    public Uri Url { get; set; } = default!;
    
    [Required]
    public string Key { get; set; } = default!;
    
    [Required]
    public int ApiRateLimit { get; set; } = default!;
    
    [Required]
    public TimeSpan ApiRateLimitWindow { get; set; } = default!;
}