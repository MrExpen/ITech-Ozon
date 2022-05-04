using Newtonsoft.Json;

namespace SiteParserLibrary.Models.Api;

public class ApiResponse
{
    [JsonProperty("suggestedTapTags")]
    public SuggestedTapTags SuggestedTapTags { get; set; }
}