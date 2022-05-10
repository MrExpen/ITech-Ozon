using System.ComponentModel.DataAnnotations;

namespace CoreLibrary.Data.Model;

public class ProductСategory
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public virtual ProductСategory? Parent { get; set; }
    public virtual ICollection<UserSearch> UserSearches { get; set; } = new List<UserSearch>();
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    public virtual ICollection<ProductСategory> Nodes { get; set; }
}