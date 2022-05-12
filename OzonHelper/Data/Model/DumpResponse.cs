namespace OzonHelper.Data.Model;

public class DumpResponse
{
    public CategoryResponse Category { get; set; }
    public List<DumpInfo> Items { get; set; } = new List<DumpInfo>();
}

public class DumpInfo
{
    public DateTime Date { get; set; }
    public IEnumerable<SearchResponse> Items { get; set; }
}

public class SearchResponse
{
    public string? Query { get; set; }
    public int SearchCount { get; set; }
    public int AddedToCard { get; set; }
    public int AveragePrice { get; set; }
}

public class CategoryResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
}