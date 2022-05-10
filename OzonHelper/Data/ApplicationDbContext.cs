using Microsoft.EntityFrameworkCore;
using OzonHelper.Data.Model;

namespace OzonHelper.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}