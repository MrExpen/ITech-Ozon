using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class PriceInfo : IPriceInfo
{
    public decimal AveragePrice { get; }
    public decimal LowestPrice { get; }
    public decimal HighestPrice { get; }
    public decimal MedianPrice { get; }

    public PriceInfo(decimal lowestPrice, decimal highestPrice, decimal averagePrice, decimal medianPrice)
    {
        AveragePrice = averagePrice;
        LowestPrice = lowestPrice;
        HighestPrice = highestPrice;
        MedianPrice = medianPrice;
    }
}