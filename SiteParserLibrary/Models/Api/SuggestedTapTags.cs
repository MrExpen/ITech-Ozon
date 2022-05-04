using Newtonsoft.Json;

namespace SiteParserLibrary.Models.Api;

public class SuggestedTapTags
{
    [JsonProperty("items")]
    public IEnumerable<Info> Items { get; set; }
}