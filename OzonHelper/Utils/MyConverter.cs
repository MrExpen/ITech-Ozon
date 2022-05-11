using OfficialOzonApi.Models.CategoriesTree;
using OfficialOzonApi.Models.Stats;
using OzonHelper.Data.Model;

namespace OzonHelper.Utils;

public static class MyConverter
{
    public static Search ConvertToSearch(this SearchResult searchResult, SiteDump? dump = null)
    {
        var result = new Search 
        {
            Dump = dump,
            DumpId = dump?.Id,
            Query = searchResult.Query,
            SearchCount = searchResult.SearchCount,
            AveragePrice = searchResult.AveragePrice,
            AddedToCard = searchResult.AddedToCardCount
        };

        result.PredictedCategories = searchResult.PredictedCategorieIds
            .Select(x => new CategoryIdStorage(x) { SearchId = result.Id }).ToArray();
        
        return result;
    }

    public static Category ConvertToCategory(this ModelNode modelNode, Category? parent = null, SiteDump? dump = null)
    {
        var result = new Category
        {
            Id = modelNode.Id,
            Name = modelNode.Name,
            ParentId = parent?.DbId,
            Parent = parent,
            DumpId = dump?.Id,
            Dump = dump
        };
        result.Children = new List<Category>(modelNode.Nodes.Select(x => x.Value.ConvertToCategory(result, dump)));
        return result;
    }
}