using Newtonsoft.Json;

namespace OfficialOzonApi.Models.SuggestedTapTags;

public class Info
{
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("cellTrackingInfo")]
    public CellTrackingInfo CellTrackingInfo { get; set; }
}