using CoreLibrary.Data;
using CoreLibrary.Data.Model;

namespace OfficialOzonApi.Utils;

public static class DbUtils
{
    public static async Task DumpDataAsync(OzonApiAdapter adapter, ApplicationDbContext dbContext, int days = 7, CancellationToken token = default)
    {
        var list = await adapter.GetAllUserSearchResultsAsync(days: 7, limit: 500, token: token);

        foreach (var result in list)
        {
            var res = new CoreLibrary.Data.Model.UserSearch
            {
                Query = result.Query,
                AveragePrice = result.AveragePrice,
                SearchCount = result.SearchCount,
                AddedToCard = result.AddedToCardCount,
                From = DateTime.Today.AddDays(-7),
                To = DateTime.Today.AddDays(-1)
            };
            foreach (var categoryId in result.PredictedCategorieIds)
            {
                var category = await dbContext.ProductСategories.FindAsync(new object?[] { categoryId }, token);
                if (category is null)
                {
                    category = new ProductСategory { Id = categoryId };
                    await dbContext.ProductСategories.AddAsync(category, token);
                }
                
                category.UserSearches.Add(res);
            }
        }

        await dbContext.SaveChangesAsync(token);
    }

    public static async Task PrepareDb(OzonApiAdapter adapter, ApplicationDbContext dbContext,
        CancellationToken token = default)
    {
        var a = (await adapter.GetNodesTree(token: token)).Select(x => x.ToProductCategory()).ToArray();
        await dbContext.ProductСategories.AddRangeAsync(a);
        await dbContext.SaveChangesAsync(token);
    }
}