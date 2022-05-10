using System.ComponentModel.DataAnnotations;

namespace OzonHelper.Data.Model;

public class CategoryIdStorage
{
    [Key] public Guid Id { get; set; }
    public int CategoryId { get; set; }

    public CategoryIdStorage() { }

    public CategoryIdStorage(int id)
    {
        CategoryId = id;
    }
}