using BrokerFinder.Cache.Services;
using BrokerFinder.Cache.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BrokerFinder.Cache.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services,
        Action<RedisOptions> optionsAction)
    {
        var options = new RedisOptions();
        optionsAction(options);

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IDatabase>(cfg =>
        {
            var multiplexer = ConnectionMultiplexer.Connect($"{options.ConnectionString}");
            
            return multiplexer.GetDatabase();
        });

        return services;
    }
}
    
    