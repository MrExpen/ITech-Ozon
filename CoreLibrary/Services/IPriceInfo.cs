namespace CoreLibrary.Services;

public interface IPriceInfo
{
    decimal AveragePrice { get; }
    decimal LowestPrice { get; }
    decimal HighestPrice { get; }
    decimal MedianPrice { get; }
}