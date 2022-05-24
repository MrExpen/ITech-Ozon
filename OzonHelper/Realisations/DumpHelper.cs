using Microsoft.EntityFrameworkCore;
using OzonHelper.Data;
using OzonHelper.Data.Model;
using OzonHelper.Services;
using System.Linq;
using OzonHelper.Utils;

namespace OzonHelper.Realisations;

public class DumpHelper : IDumpsHelper
{
    private readonly ApplicationDbContext _db;

    public DumpHelper(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DumpCategoryResponse>> GetDumps(string categoryName, CancellationToken token = default)
    {
        var categories = _db.GetCategoriesByName(categoryName);
        var result = new List<DumpCategoryResponse>();
        foreach (var searchResponse in (await _GetDumps(categories, token)).GroupBy(x => x.Query))
        {
            result.Add(new DumpCategoryResponse
            {
                Query = searchResponse.Key,
                Data = searchResponse.Select(x => new DumpGraphicPoint
                {
                    Date = x.Date,
                    AveragePrice = x.AveragePrice,
                    SearchCount = x.SearchCount,
                    AddedToCard = x.AddedToCard
                })
            });
        }
        
        
        return result;
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

    private async Task<IEnumerable<SearchResponse>> _GetDumps(List<Category> categories,
        CancellationToken token = default)
    {
        var list = new List<SearchResponse>();

        foreach (var category in categories)
        {
            list.AddRange(await _GetDumps(category, token));
        }

        return list;
    }

    private async Task<IEnumerable<SearchResponse>> _GetDumps(Category? category, CancellationToken token = default)
    {
        if (category is null)
        {
            return Enumerable.Empty<SearchResponse>();
        }
        var list = new List<SearchResponse>();
        foreach (var searchId in _db.CategoryIdStorage.Where(x => x.CategoryId == category.Id).Select(x => x.SearchId))
        {
            var search = await _db.Searches.FindAsync(searchId);
            list.Add(new SearchResponse
            {
                Date = search.Dump.Date,
                Query = search.Query,
                AveragePrice = search.AveragePrice,
                SearchCount = search.SearchCount,
                AddedToCard = search.AddedToCard
            });
        }

        return (await _GetDumps(category.Parent, token)).Concat(list);
    }
}