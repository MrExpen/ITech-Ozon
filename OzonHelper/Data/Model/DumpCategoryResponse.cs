namespace OzonHelper.Data.Model;

public class DumpCategoryResponse
{
    public string? Query { get; set; }
    public IEnumerable<DumpGraphicPoint> Data { get; set; }
}

public class DumpGraphicPoint
{
    public DateTime? Date { get; set; }
    public int SearchCount { get; set; }
    public int AddedToCard { get; set; }
    public int AveragePrice { get; set; }
}