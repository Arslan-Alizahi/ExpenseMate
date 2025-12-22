using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // SQLite doesn't support decimal, use TEXT or REAL
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price).HasConversion<double>();
        });
    }
}
