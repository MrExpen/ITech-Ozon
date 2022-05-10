using CoreLibrary.Data.Model;
using RestSharp;

namespace OfficialOzonApi.Models.CategoriesTree;

public class MyNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public MyNode? Parent { get; set; }
    public IEnumerable<MyNode> Nodes { get; set; }

    public IEnumerable<MyNode> GetAllNodesRecursion()
    {
        return Nodes.SelectMany(x => x.GetAllNodesRecursion()).Append(this);
    }

    public IEnumerable<int> GetIdsToSearch()
    {
        if (Nodes.Any())
        {
            return Nodes.SelectMany(x => x.GetIdsToSearch());
        }

        return new[] { Id };
    }

    public override string ToString()
    {
        return $"{Name}[{Id}], children={Nodes.Count()}";
    }

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
}