using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class DumpsInfoResult : IDumpsInfoResult
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<IDumpsInfo> Items { get; set; } = new List<IDumpsInfo>();
}