using System.ComponentModel.DataAnnotations;

namespace OzonHelper.Data.Model;

public class Category
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    
    public int? ParentId { get; set; }
    public virtual Category? Parent { get; set; }

    public virtual ICollection<Category> Children { get; set; } = new List<Category>();
}