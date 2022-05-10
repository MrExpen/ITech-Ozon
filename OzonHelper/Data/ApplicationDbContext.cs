using Microsoft.EntityFrameworkCore;
using OzonHelper.Data.Model;

namespace OzonHelper.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<SiteDump> SiteDumps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId);

        modelBuilder.Entity<Search>()
            .HasMany(x => x.PredictedCategories)
            .WithOne()
            .HasForeignKey(x => x.SearchId);

        modelBuilder.Entity<SiteDump>()
            .HasMany(x => x.Searches)
            .WithOne(x => x.Dump).OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.DumpId);

        modelBuilder.Entity<SiteDump>()
            .HasMany(x => x.Categories)
            .WithOne(x => x.Dump)
            .HasForeignKey(x => x.DumpId);
    }
}