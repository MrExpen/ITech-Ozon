using Microsoft.EntityFrameworkCore;
using OzonHelper.Data;
using OzonHelper.Data.Model;
using OzonHelper.Services;
using System.Linq;

namespace OzonHelper.Realisations;

public class DumpHelper : IDumpsHelper
{
    private readonly ApplicationDbContext _db;

    public DumpHelper(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DumpResponse>> GetDumps(string categoryName, CancellationToken token = default)
    {
        var categories = await _db.Categories.Where(x => x.Name == categoryName).ToListAsync(token);
        return await _GetDumps(categories, token);

        // var category = await _db.Categories.FindAsync(new object?[] { categoryId }, token);
        // var result = new DumpResponse { Category = new CategoryResponse { Id = category.Id, Name = category.Name } };
        //
        // var searches = _db.CategoryIdStorage.Where(x => x.CategoryId == categoryId)
        //     .AsEnumerable().Select(x => _db.Searches.Find(x.SearchId));
        // foreach (var search in searches.GroupBy(x => x.DumpId))
        // {
        //     result.Items.Add(new DumpInfo
        //     {
        //         Date = search.FirstOrDefault().Dump.Date, Items = search.Select(x => new SearchResponse
        //         {
        //             Query = x.Query,
        //             AveragePrice = x.AveragePrice,
        //             SearchCount = x.SearchCount,
        //             AddedToCard = x.AddedToCard
        //         })
        //     });
        // }
        // return result;
    }

    public async Task<IEnumerable<DumpInfo>> GetDumpsByQuery(string query, CancellationToken token = default)
    {
        var result = new List<DumpInfo>();

        var searches = _db.Searches.AsEnumerable()
            .Where(x => x.Query.ToLowerInvariant().Contains(query.ToLowerInvariant())).ToList();

        foreach (var search in searches.GroupBy(x => x.DumpId))
        {
            result.Add(new DumpInfo
            {
                Date = search.FirstOrDefault().Dump.Date, Items = search.Select(x => new SearchResponse
                {
                    Query = x.Query,
                    AveragePrice = x.AveragePrice,
                    SearchCount = x.SearchCount,
                    AddedToCard = x.AddedToCard
                })
            });
        }

        return result;
    }

    private async Task<IEnumerable<DumpResponse>> _GetDumps(List<Category> categories,
        CancellationToken token = default)
    {
        var list = new List<DumpResponse>();
        foreach (var category in categories)
        {
            list.AddRange(await _GetDumps(category, token));
        }

        return list;
    }

    private async Task<IEnumerable<DumpResponse>> _GetDumps(Category? category, CancellationToken token = default)
    {
        if (category is null)
        {
            return Enumerable.Empty<DumpResponse>();
        }

        var result = new DumpResponse { Category = new CategoryResponse { Id = category.Id, Name = category.Name } };

        var searches = _db.CategoryIdStorage.Where(x => x.CategoryId == category.Id)
            .AsEnumerable().Select(x => _db.Searches.Find(x.SearchId));
        foreach (var search in searches.GroupBy(x => x.DumpId))
        {
            result.Items.Add(new DumpInfo
            {
                Date = search.FirstOrDefault().Dump.Date, Items = search.Select(x => new SearchResponse
                {
                    Query = x.Query,
                    AveragePrice = x.AveragePrice,
                    SearchCount = x.SearchCount,
                    AddedToCard = x.AddedToCard
                })
            });
        }

        return (await _GetDumps(category.Parent, token)).Append(result);

    }
}