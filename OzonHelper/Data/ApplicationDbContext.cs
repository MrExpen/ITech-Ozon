using Microsoft.EntityFrameworkCore;
using OzonHelper.Data.Model;

namespace OzonHelper.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<SiteDump> SiteDumps { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Search> Searches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(x => x.Parent)
            .WithMany(x => x.Children);

        modelBuilder.Entity<Search>()
            .HasMany(x => x.PredictedCategories)
            .WithOne();

        modelBuilder.Entity<SiteDump>()
            .HasMany(x => x.Searches)
            .WithOne(x => x.Dump)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<SiteDump>()
            .HasMany(x => x.Categories)
            .WithOne(x => x.Dump)
            .OnDelete(DeleteBehavior.Cascade);
    }
}