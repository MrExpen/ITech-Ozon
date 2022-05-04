using Newtonsoft.Json;

namespace SiteParserLibrary.Models.Api;

public class Info
{
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("cellTrackingInfo")]
    public CellTrackingInfo CellTrackingInfo { get; set; }
}