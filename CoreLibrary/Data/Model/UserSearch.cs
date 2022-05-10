using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Data.Model;

public class UserSearch
{
    [Key] public Guid Id { get; set; }
    public string Query { get; set; }
    public int SearchCount { get; set; }
    public int AddedToCard { get; set; }
    public int AveragePrice { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    
    public virtual ICollection<ProductСategory> PredictedCategories { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    // public virtual ICollection<UserSearchOneToMany> PredictedCategoriesHelper { get; set; } = new List<UserSearchOneToMany>();
    // [NotMapped]
    // public IEnumerable<int> PredictedCategories => PredictedCategoriesHelper.Select(x => x.CategoryId);

    public UserSearch() {}
}