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

    public static List<Category> GetCategoriesByName(this ApplicationDbContext db, string pattern, int minComp = 75)
    {
        return db.Categories.AsEnumerable().Where(x => _CompareString(pattern, x.Name) > minComp)
            .DistinctBy(x => x.Id).ToList();
    }

    public static IEnumerable<Category> GetCategoriesToRoot(Category? category)
    {
        if (category is null)
        {
            return Enumerable.Empty<Category>();
        }

        return GetCategoriesToRoot(category.Parent).Append(category);
    }

    public static IEnumerable<Category> GetAllCategoriesToRootByName(this ApplicationDbContext db, string pattern,
        int minComp = 75)
    {
        return GetCategoriesByName(db, pattern, minComp).SelectMany(GetCategoriesToRoot).DistinctBy(x => x.Id);
    }

    public static IEnumerable<Guid> GetSearchesIdsInCategories(this ApplicationDbContext db, IEnumerable<Category> categories)
    {
        var result = new HashSet<Guid>();

        return categories.SelectMany(x => GetSearchesInCategory(db, x));
    }

    public static IEnumerable<Guid> GetSearchesInCategory(this ApplicationDbContext db, Category? category)
    {
        if (category is null)
        {
            return Enumerable.Empty<Guid>();
        }

        return db.CategoryIdStorage.Select(x => x.SearchId);
    }
    
    private static int _CompareString(string patter, string findIn)
    {
        return patter.ToLower() == findIn.ToLower() ? 100 : 0;
    }
}