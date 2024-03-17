using Microsoft.Extensions.DependencyInjection;

namespace BrokerFinder.Cache.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services,
        Action<RedisOptions> optionsAction)
    {
        var redisOptions = new RedisOptions();
        optionsAction(redisOptions);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{redisOptions.ConnectionString}";
            options.InstanceName = "BrokerFinder";
        });

        return services;
    }
}
    
    