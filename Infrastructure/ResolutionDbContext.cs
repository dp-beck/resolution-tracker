using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ResolutionDbContext(DbContextOptions<ResolutionDbContext> options) : DbContext(options)
{
    public DbSet<Resolution>? Resolutions { get; set; } = null;
    public DbSet<ResolutionCategory>? ResolutionCategories { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Resolution>().HasData(
            );
    }
}