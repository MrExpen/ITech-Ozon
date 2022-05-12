using OzonHelper.Data;
using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class DumpHelper : IDumpsHelper
{
    private readonly ApplicationDbContext _db;

    public DumpHelper(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IDumpsInfoResult> GetDumps(int categoryId, CancellationToken token = default)
    {
        var category = await _db.Categories.FindAsync(new object?[] { categoryId }, token);
        var result = new DumpsInfoResult { CategoryId = category.Id, CategoryName = category.Name };
        
        var searches = _db.CategoryIdStorage.Where(x => x.CategoryId == categoryId)
            .AsEnumerable().Select(x => _db.Searches.Find(x.SearchId));
        foreach (var search in searches)
        {
            result.Items.Add(new DumpsInfo
            {
                Date = search.Dump.Date,
                Query = search.Query,
                AveragePrice = search.AveragePrice,
                SearchCount = search.SearchCount,
                AddedToCard = search.AddedToCard
            });
        }

        return result;
    }
}