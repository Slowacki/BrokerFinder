using System.ComponentModel.DataAnnotations;

namespace BrokerFinder.Cache.Configuration;

public class RedisOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}