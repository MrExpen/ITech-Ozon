using Microsoft.EntityFrameworkCore;
using OzonHelper.Data;
using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class DbNamingHelper : INamingHelper
{
    private readonly ApplicationDbContext _db;

    public DbNamingHelper(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<string> GetSuggestions(string name)
        => GetSuggestionsAsync(name).GetAwaiter().GetResult();

    public async Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default)
    {
        return await _db.Searches.Where(x => x.Query.StartsWith(name)).Select(x => x.Query).Distinct()
            .ToArrayAsync(token);
    }
}