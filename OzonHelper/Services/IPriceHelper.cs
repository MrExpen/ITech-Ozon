namespace OzonHelper.Services;

public interface IPriceHelper<TPriceInfo> where TPriceInfo : IPriceInfo, new()
{
    Task<IEnumerable<TPriceInfo>> GetPriceAsync(string category, CancellationToken token = default);
    Task<IEnumerable<TPriceInfo>> GetPriceAsync(int categoryId, CancellationToken token = default);
    
}