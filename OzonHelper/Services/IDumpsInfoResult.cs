namespace OzonHelper.Services;

public interface IDumpsInfoResult
{
    int CategoryId { get; set; }
    string CategoryName { get; set; }
    List<IDumpsInfo> Items { get; set; }
}