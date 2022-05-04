using Newtonsoft.Json;

namespace OfficialOzonApi.Models.Stats;

public class SearchResult
{
    [JsonProperty("avgCaRub")]
    public int AveragePrice { get; set; }
    
    [JsonProperty("avgCountItems")]
    public double AverageCountItems { get; set; }
    
    [JsonProperty("ca")]
    public double AddedToCardProbability { get; set; }
    
    [JsonProperty("caQty")]
    public int CaQty { get; set; }
    
    [JsonProperty("caRub")]
    public long CaRub { get; set; }
    
    [JsonProperty("count")]
    public int SearchCount { get; set; }
    
    [JsonProperty("itemsViews")]
    public double ItemsViews{ get; set; }
    
    [JsonProperty("meanBid")]
    public double MeanBid{ get; set; }
    
    [JsonProperty("predictedCategories")]
    public IEnumerable<int> PredictedCategorieIds{ get; set; }
    
    [JsonProperty("query")]
    public string Query{ get; set; }
    
    [JsonProperty("softQueryCount")]
    public int AddedToCardCount { get; set; }
    
    [JsonProperty("softQueryShare")]
    public double SoftQueryShare{ get; set; }
    
    [JsonProperty("uniqQueriesWCa")]
    public int UniqQueriesWCa{ get; set; }
    
    [JsonProperty("uniqSellers")]
    public double UniqSellers{ get; set; }
    
    [JsonProperty("usersWithoutInterectionCount")]
    public int UsersWithoutInterectionCount{ get; set; }
    
    [JsonProperty("usersWithoutInterectionShare")]
    public double UsersWithoutInterectionShare{ get; set; }
    
    [JsonProperty("zrCount")]
    public int ZrCount{ get; set; }
    
    [JsonProperty("zrShare")]
    public double ZrShare{ get; set; }
}