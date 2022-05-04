namespace OfficialOzonApi.Models.CategoriesTree;

public class MyNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public MyNode? Parent { get; set; }
    public IEnumerable<MyNode> Nodes { get; set; }

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
}