using BrokerFinder.Funda.Models;
using Refit;

namespace BrokerFinder.Funda.Apis;

public interface IListingsApi
{
    [Get("/")]
    Task<GetListingsResponse> GetListingsAsync([Query] string type,
        [Query][AliasAs("zo")] string query,
        [Query] int page,
        [Query] int pageSize = 25,
        CancellationToken cancellationToken = default);
}