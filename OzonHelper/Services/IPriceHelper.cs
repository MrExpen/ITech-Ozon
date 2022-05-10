namespace OzonHelper.Services;

public interface IPriceHelper
{
    IPriceInfo GetPrice(string name, string category);
    Task<IPriceInfo> GetPriceAsync(string name, string category, CancellationToken token = default);
}