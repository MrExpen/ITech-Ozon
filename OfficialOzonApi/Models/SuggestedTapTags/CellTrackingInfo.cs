using Newtonsoft.Json;

namespace OfficialOzonApi.Models.SuggestedTapTags;

public class CellTrackingInfo
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("index")]
    public int Index { get; set; }
    
    [JsonProperty("searchString")]
    public string SearchString { get; set; }
    
    [JsonProperty("suggestType")]
    public string SuggestType { get; set; }
    
    [JsonProperty("suggestValue")]
    public string SuggestValue { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
}