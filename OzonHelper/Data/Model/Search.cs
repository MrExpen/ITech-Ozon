using System.ComponentModel.DataAnnotations;

namespace OzonHelper.Data.Model;

public class Search
{
    [Key] public Guid Id { get; set; }
    public string? Query { get; set; }
    public int SearchCount { get; set; }
    public int AddedToCard { get; set; }
    public int AveragePrice { get; set; }

    public virtual ICollection<CategoryIdStorage> PredictedCategories { get; set; } = new List<CategoryIdStorage>();
}