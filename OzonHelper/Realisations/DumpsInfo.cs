using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class DumpsInfo : IDumpsInfo
{
    public DateTime Date { get; set; }
    public string? Query { get; set; }
    public int SearchCount { get; set; }
    public int AddedToCard { get; set; }
    public int AveragePrice { get; set; }
}