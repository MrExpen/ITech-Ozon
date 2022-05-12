using Microsoft.EntityFrameworkCore;
using OzonHelper.Data;
using OzonHelper.Data.Model;
using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class DbPriceHelper : IPriceHelper<PriceInfo>
{
    private readonly ApplicationDbContext _db;

    public DbPriceHelper(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<PriceInfo>> GetPriceAsync(string category, CancellationToken token = default)
    {
        var categories = await _db.Categories.Where(x => x.Name == category).ToListAsync(token);
        return await _GetPriceAsync(categories, token);
    }

    public async Task<IEnumerable<PriceInfo>> GetPriceAsync(int categoryId, CancellationToken token = default)
    {
        var category = await _db.Categories.FindAsync(new object?[] { categoryId }, token);
        return await _GetPriceAsync(new List<Category> { category }, token);
    }
    
    private async Task<IEnumerable<PriceInfo>> _GetPriceAsync(List<Category> categories, CancellationToken token = default)
    {
        var list = new List<PriceInfo>();
        var tmp = new List<Guid>();
        foreach (var category in categories)
        {
            tmp.AddRange(_db.CategoryIdStorage.Where(x => x.CategoryId == category.Id).Select(x => x.SearchId));
        }
        foreach (var guid in tmp)
        {
            var search = await _db.Searches.FindAsync(guid);
            list.Add(new PriceInfo
                { Date = search.Dump.Date, Query = search.Query, AveragePrice = search.AveragePrice });
        }

        return list.OrderBy(x => x.Date);
    }
}