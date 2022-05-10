using CoreLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<UserSearch> UserSearches { get; set; }
    public DbSet<ProductСategory> ProductСategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSearch>()
            .HasMany(x => x.PredictedCategories)
            .WithMany(x => x.UserSearches)
            .UsingEntity<Enrollment>(
                j => j
                    .HasOne(x => x.ProductСategory)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.ProductСategoryId),
                j => j
                    .HasOne(x => x.UserSearch)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.UserSearchId)
            );
    }
}