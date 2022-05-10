using OfficialOzonApi.Models.CategoriesTree;
using OfficialOzonApi.Models.Stats;
using OzonHelper.Data.Model;

namespace OzonHelper.Utils;

public static class MyConverter
{
    public static Search ConvertToSearch(this SearchResult searchResult) 
        => new Search 
        {
            Query = searchResult.Query,
            SearchCount = searchResult.SearchCount,
            AveragePrice = searchResult.AveragePrice,
            AddedToCard = searchResult.AddedToCardCount,
            PredictedCategories = searchResult.PredictedCategorieIds.Select(x => new CategoryIdStorage(x)).ToArray()
        };

    public static Category ConvertToCategory(this ModelNode modelNode, Category? parent = null)
    {
        var result = new Category
        {
            Id = modelNode.Id,
            Name = modelNode.Name,
            ParentId = modelNode.ParentId,
            Parent = parent
        };
        result.Children = new List<Category>(modelNode.Nodes.Select(x => x.Value.ConvertToCategory(result)));
        return result;
    }
}