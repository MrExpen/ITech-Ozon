using Microsoft.EntityFrameworkCore;

namespace OzonHelper.Utils;

public static class DbUtils
{
    public static void Clear<T>(this DbSet<T> dbSet) where T : class
        => dbSet.RemoveRange(dbSet.ToArray());
    
    
    public static async Task ClearAsync<T>(this DbSet<T> dbSet) where T : class
        => dbSet.RemoveRange(await dbSet.ToArrayAsync());
}