using Newtonsoft.Json;

namespace OfficialOzonApi.Models.Stats;

public struct SortingParams
{
    [JsonProperty("attribute")]
    public string Attribute { get; set; }
    
    [JsonProperty("order")]
    public string Order { get; set; }

    public SortingParams(string attribute, string order)
    {
        Attribute = attribute;
        Order = order;
    }
}