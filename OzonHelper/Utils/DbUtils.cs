using Microsoft.EntityFrameworkCore;
using OzonHelper.Data;
using OzonHelper.Data.Model;

namespace OzonHelper.Utils;

public static class DbUtils
{
    public static void Clear<T>(this DbSet<T> dbSet) where T : class
        => dbSet.RemoveRange(dbSet.ToArray());
    
    
    public static async Task ClearAsync<T>(this DbSet<T> dbSet) where T : class
        => dbSet.RemoveRange(await dbSet.ToArrayAsync());

    public static List<Category> GetCategoriesByName(this ApplicationDbContext db, string pattern, int minComp = 85, int take = 5)
    {
        return db.Categories.AsEnumerable().Where(x => _CompareString(pattern, x.Name) > minComp)
            .DistinctBy(x => x.Id)
            .OrderByDescending(x => _CompareString(pattern, x.Name))
            .Take(take)
            .ToList();
    }

    private static int _CompareString(string pattern, string findIn)
    {
        return FuzzySharp.Fuzz.PartialTokenSetRatio(pattern.ToLower(), findIn.ToLower());
        return pattern.ToLower() == findIn.ToLower() ? 100 : 0;
    }
}