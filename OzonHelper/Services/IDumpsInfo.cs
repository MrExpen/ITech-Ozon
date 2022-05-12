namespace OzonHelper.Services;

public interface IDumpsInfo
{
    DateTime Date { get; set; }
    string? Query { get; set; }
    int SearchCount { get; set; }
    int AddedToCard { get; set; } 
    int AveragePrice { get; set; } 
}