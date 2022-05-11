using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzonHelper.Data.Model;

public class Search
{
    [Key] public Guid Id { get; set; }
    public string? Query { get; set; }
    public int SearchCount { get; set; }
    public int AddedToCard { get; set; }
    public int AveragePrice { get; set; }

    public Guid? DumpId { get; set; }
    public virtual SiteDump? Dump { get; set; }
    
    public virtual ICollection<CategoryIdStorage> PredictedCategories { get; set; } = new List<CategoryIdStorage>();
}