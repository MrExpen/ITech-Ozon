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
            list.AddRange(await _GetPriceAsync(category, token));
        }
        
        return list.OrderBy(x => x.Date);
    }

    private async Task<IEnumerable<PriceInfo>> _GetPriceAsync(Category? category, CancellationToken token = default)
    {
        if (category is null)
        {
            return Enumerable.Empty<PriceInfo>();
        }
        var list = new List<PriceInfo>();
        foreach (var searchId in _db.CategoryIdStorage.Where(x => x.CategoryId == category.Id).Select(x => x.SearchId))
        {
            var search = await _db.Searches.FindAsync(searchId);
            list.Add(new PriceInfo
                { Date = search.Dump.Date, Query = search.Query, AveragePrice = search.AveragePrice });
        }

        if (list.Count == 0)
        {
            return await _GetPriceAsync(category.Parent, token);
        }

        return list;
    }
}