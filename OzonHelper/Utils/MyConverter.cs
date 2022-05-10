using OfficialOzonApi.Models.CategoriesTree;
using OfficialOzonApi.Models.Stats;
using OzonHelper.Data.Model;

namespace OzonHelper.Utils;

public static class MyConverter
{
    public static Search ConvertToSearch(this SearchResult result)
    {
        throw new NotImplementedException();
        /*
        var result = new CoreLibrary.Data.Model.UserSearch
        {
            Query = DbLoggerCategory.Query,
            AveragePrice = AveragePrice,
            SearchCount = SearchCount,
            AddedToCard = AddedToCardCount,
            PredictedCategories =
                PredictedCategorieIds.Select(x => new ProductСategory { Id = x }).ToList(),
            From = from,
            To = to
        };
        return result;
        */
    }

    public static Category ConvertToCategory(this ModelNode modelNode, Category? parent = null)
    {
        throw new NotImplementedException();
        /*
         
         public MyNode(ModelNode node, MyNode? parent = null)
        {
            Id = node.Id;
            Name = node.Name;
            ParentId = node.ParentId;
            Parent = parent;
            Nodes = node.Nodes.Select(x => new MyNode(x.Value, this)).ToArray();
        }

        public ProductСategory ToProductCategory(ProductСategory? parent = null)
        {
            var result = new ProductСategory
            {
                Id = Id,
                Name = Name,
                Parent = parent
            };
            result.Nodes = Nodes.Select(x => x.ToProductCategory(result)).ToArray();
            return result;
        }
          
        */
    }
}