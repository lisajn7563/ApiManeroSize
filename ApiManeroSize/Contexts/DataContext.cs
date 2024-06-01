using ApiManeroSize.Entites;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroSize.Contexts;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<SizeEntity> Size { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SizeEntity>()
            .ToContainer("Sizes")
            .HasPartitionKey(x => x.PartitionKey);
    }
}
