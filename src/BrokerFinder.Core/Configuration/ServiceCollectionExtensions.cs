using BrokerFinder.Core.Services;
using BrokerFinder.Core.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BrokerFinder.Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IBrokersService, BrokersService>();
        
        return services;
    }
}