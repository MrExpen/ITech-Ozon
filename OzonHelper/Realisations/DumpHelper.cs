using OzonHelper.Data;
using OzonHelper.Data.Model;
using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class DumpHelper : IDumpsHelper
{
    private readonly ApplicationDbContext _db;

    public DumpHelper(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DumpResponse> GetDumps(int categoryId, CancellationToken token = default)
    {
        var category = await _db.Categories.FindAsync(new object?[] { categoryId }, token);
        var result = new DumpResponse { Category = new CategoryResponse { Id = category.Id, Name = category.Name } };

        var searches = _db.CategoryIdStorage.Where(x => x.CategoryId == categoryId)
            .AsEnumerable().Select(x => _db.Searches.Find(x.SearchId));
        foreach (var search in searches.GroupBy(x => x.DumpId))
        {
            result.Items.Add(new DumpInfo{Date = search.FirstOrDefault().Dump.Date, Items = search.Select(x=> new SearchResponse
            {
                Query = x.Query,
                AveragePrice = x.AveragePrice,
                SearchCount = x.SearchCount,
                AddedToCard = x.AddedToCard
            })});
        }

        return result;
    }
}