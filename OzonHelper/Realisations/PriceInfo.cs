using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class PriceInfo : IPriceInfo
{
    public DateTime Date { get; set; }
    public string? Query { get; set; }
    public int AveragePrice { get; set; }
}