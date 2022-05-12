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
    {
        return _db.Searches.AsEnumerable().Where(x => x.Query.ToLowerInvariant().Contains(name.ToLowerInvariant())).Select(x => x.Query).Distinct()
            .ToArray();
    }

    public async Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default)
        => GetSuggestions(name);
}