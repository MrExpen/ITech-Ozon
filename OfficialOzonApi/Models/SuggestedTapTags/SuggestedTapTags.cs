using Newtonsoft.Json;

namespace OfficialOzonApi.Models.SuggestedTapTags;

public class SuggestedTapTags
{
    [JsonProperty("items")]
    public IEnumerable<Info> Items { get; set; }
}