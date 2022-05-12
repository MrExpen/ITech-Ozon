using Microsoft.EntityFrameworkCore;
using OzonHelper.Data.Model;

namespace OzonHelper.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<SiteDump> SiteDumps { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Search> Searches { get; set; }
    public DbSet<CategoryIdStorage> CategoryIdStorage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Search>()
            .HasMany(x => x.PredictedCategories)
            .WithOne();

        modelBuilder.Entity<SiteDump>()
            .HasMany(x => x.Searches)
            .WithOne(x => x.Dump)
            .OnDelete(DeleteBehavior.Cascade);
    }
}