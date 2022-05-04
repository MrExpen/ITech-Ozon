using CoreLibrary.Realisations;
using CoreLibrary.Services;

namespace TestsLibrary.Services;

public class PriceHelper : IPriceHelper
{
    public IPriceInfo GetPrice(string name, string category)
        => new PriceInfo(
            Random.Shared.Next(4000),
            Random.Shared.Next(4000, 20000),
            Random.Shared.Next(4000, 10000),
            Random.Shared.Next(15000)
            );

    public Task<IPriceInfo> GetPriceAsync(string name, string category, CancellationToken token = default)
        => Task.FromResult(GetPrice(name, category));
}