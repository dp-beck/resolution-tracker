using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ResolutionDbContext(DbContextOptions<ResolutionDbContext> options) : DbContext(options)
{
    public DbSet<Resolution>? Resolutions { get; set; } = null;
    public DbSet<ResolutionCategory>? ResolutionCategories { get; set; } = null;
}