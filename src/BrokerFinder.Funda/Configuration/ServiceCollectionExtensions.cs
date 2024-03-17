using System.Threading.RateLimiting;
using BrokerFinder.Funda.Apis;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.RateLimiting;
using Polly.Retry;
using Refit;

namespace BrokerFinder.Funda.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFunda(this IServiceCollection services,
        Action<FundaOptions> optionsAction)
    {
        var options = new FundaOptions();
        optionsAction(options);

        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        
        var refitSettings = new RefitSettings { ContentSerializer = new NewtonsoftJsonContentSerializer(serializerSettings) };

        services
            .AddRefitClient<IListingsApi>(refitSettings)
            .ConfigureHttpClient((sp, c) => c.BaseAddress = new Uri(options.Url, options.Key))
            .AddResilienceHandler("RateLimitingPolicy", builder =>
            {
                builder.AddRetry(
                    new RetryStrategyOptions<HttpResponseMessage>
                    {
                        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                            .Handle<RateLimiterRejectedException>(),
                        Delay = TimeSpan.FromSeconds(1),
                        MaxRetryAttempts = 6,
                        UseJitter = false,
                        BackoffType = DelayBackoffType.Exponential
                    })
                    .AddRateLimiter(new SlidingWindowRateLimiter(
                    new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = options.ApiRateLimit,
                        Window = options.ApiRateLimitWindow,
                        AutoReplenishment = true,
                        SegmentsPerWindow = 3
                    }));
            });
        
        return services;
    }
}