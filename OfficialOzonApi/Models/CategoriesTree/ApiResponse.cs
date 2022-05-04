using Newtonsoft.Json;

namespace OfficialOzonApi.Models.CategoriesTree;

public class ApiResponse
{
    [JsonProperty("result")]
    public Dictionary<int, ModelNode> Result { get; set; }
}