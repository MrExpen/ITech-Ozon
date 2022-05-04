using Newtonsoft.Json;

namespace OfficialOzonApi.Models.Stats;

public class ApiResponse
{
    [JsonProperty("data")]
    public IEnumerable<SearchResult> Data { get; set; }
    
    [JsonProperty("offset")]
    public int Offset { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }
}