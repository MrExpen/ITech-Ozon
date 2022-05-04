using Newtonsoft.Json;

namespace OfficialOzonApi.Models.CategoriesTree;

public class ModelNode
{
    [JsonProperty("description_category_ids")]
    public IEnumerable<int> DescriptionCategoryIds { get; set; }
    
    [JsonProperty("disabled")]
    public bool Disabled { get; set; }
    
    [JsonProperty("has_disabled")]
    public bool HasDisabled { get; set; }
    
    [JsonProperty("Id")]
    public int Id { get; set; }
    
    [JsonProperty("is_oversize")]
    public bool IsOversize { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("parent_id")]
    public int ParentId { get; set; }
    
    [JsonProperty("nodes")]
    public Dictionary<int, ModelNode> Nodes { get; set; }

    public override string ToString()
    {
        return $"{Name}[{Id}], children={Nodes.Count}";
    }
}