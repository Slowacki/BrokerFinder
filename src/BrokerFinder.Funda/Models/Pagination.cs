using Newtonsoft.Json;

namespace BrokerFinder.Funda.Models;

public record Pagination
{
    [JsonProperty("HuidigePagina")]
    public int CurrentPage { get; set; }
    
    [JsonProperty("AantalPaginas")]
    public int PagesCount { get; set; }
}