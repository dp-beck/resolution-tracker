using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResolutionCategoryRepository (ResolutionDbContext context) : IResolutionCategoryRepository
{
    public async Task<IEnumerable<ResolutionCategory>> GetAllAsync()
    {
        return await context.ResolutionCategories.ToListAsync();
    }

    public async Task<ResolutionCategory?> FindByIdAsync(int id)
    {
        return await context.ResolutionCategories.FindAsync(id);
    }

    public async Task<ResolutionCategory?> FindByNameAsync(string name)
    {
        return await context.ResolutionCategories.FirstOrDefaultAsync(
            c => c.Name == name);
    }

    public async Task AddAsync(ResolutionCategory category)
    {
        await context.ResolutionCategories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ResolutionCategory updatedCategory)
    {
        var oldCategory = await context.ResolutionCategories.FindAsync();
        if (oldCategory != null) context.Entry(oldCategory).CurrentValues.SetValues(updatedCategory);
        await context.SaveChangesAsync();    }

    public async Task DeleteAsync(ResolutionCategory category)
    {
        context.ResolutionCategories.Remove(category);
        await context.SaveChangesAsync();
    }
}