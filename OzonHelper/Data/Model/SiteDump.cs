using System.ComponentModel.DataAnnotations;

namespace OzonHelper.Data.Model;

public class SiteDump
{
    [Key] public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public DumpWeeks DumpWeeks { get; set; }
    
    public virtual List<Search> Searches { get; set; } = new List<Search>();
    
    public virtual List<Category> Categories { get; set; } = new List<Category>();
}