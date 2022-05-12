namespace OzonHelper.Services;

public interface IPriceInfo
{
    DateTime Date { get; set; }
    string Query { get; set; }
    int AveragePrice { get; set; }
}

