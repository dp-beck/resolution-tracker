using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ResolutionDbContext(DbContextOptions<ResolutionDbContext> options) : DbContext(options)
{
    public virtual DbSet<Resolution> Resolutions { get; set; }
    public virtual DbSet<ResolutionCategory> ResolutionCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<ResolutionCategory>()
            .HasMany(r => r.Resolutions)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .HasPrincipalKey(r => r.Id);
        
        // Seed Date for Testing Purposes
        modelBuilder.Entity<ResolutionCategory>().ToTable("ResolutionCategories");
        _ = modelBuilder.Entity<ResolutionCategory>().HasData(
            new ResolutionCategory { Id = 1, Name = "Health" },
            new ResolutionCategory { Id = 2, Name = "Career" },
            new ResolutionCategory { Id = 3, Name = "Hobbies" }
        );
        
        modelBuilder.Entity<Resolution>().ToTable("Resolutions");
        _ = modelBuilder.Entity<Resolution>().HasData(
            new Resolution { Id = 1, Title = "Lose weight", Description = "Lose 10 pounds", Goal = 10, CategoryId = 1},
            new Resolution { Id = 2, Title = "Gain muscle", Description = "Do 1,000 pushups", Goal = 1000, CategoryId = 1},
            new Resolution { Id = 3, Title = "Learn to play Guitar", Description = "Practice 1,500 minutes", Goal = 1500, CategoryId = 3},
            new Resolution { Id= 4, Title = "Earn new Certificates", Description = "Earn a Certificate in AWS", CategoryId = 2},
            new Resolution { Id = 5, Title = "Read more", Description = "Read 10 books", Goal = 10, CategoryId = 3}
        );
        
        base.OnModelCreating(modelBuilder);

    }
}