using System.ComponentModel.DataAnnotations;

namespace BrokerFinder.Funda.Configuration;

public class FundaOptions
{
    [Required]
    public Uri Url { get; set; } = default!;
    
    [Required]
    public string Key { get; set; } = default!;
}