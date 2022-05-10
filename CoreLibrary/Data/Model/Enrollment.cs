namespace CoreLibrary.Data.Model;

public class Enrollment
{
    public Guid UserSearchId { get; set; }
    public virtual UserSearch UserSearch { get; set; }
 
    public int ProductСategoryId { get; set; }
    public virtual ProductСategory ProductСategory { get; set; }
}