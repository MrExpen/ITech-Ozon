using Newtonsoft.Json;

namespace OfficialOzonApi.Models.SuggestedTapTags;

public class ApiResponse
{
    [JsonProperty("suggestedTapTags")]
    public SuggestedTapTags SuggestedTapTags { get; set; }
}