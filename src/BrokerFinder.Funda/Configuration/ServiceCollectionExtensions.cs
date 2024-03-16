using System.Net;
using BrokerFinder.Funda.Apis;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Extensions.Http;
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

        
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            // The API returns 401 when the rate limit is reached instead of the usual 429
            .OrResult(m => m.StatusCode == HttpStatusCode.Unauthorized)
            //.OrResult(m => m.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        services
            .AddRefitClient<IListingsApi>(refitSettings)
            .ConfigureHttpClient((sp, c) => c.BaseAddress = new Uri(options.Url, options.Key))
            .AddPolicyHandler(retryPolicy);
        
        return services;
    }
}