using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

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

    public async Task UpdateAsync(int id, ResolutionCategoryDto categoryDto)
    {
        var oldCategory = await context.ResolutionCategories.FindAsync(id);
        if (oldCategory != null) context.Entry(oldCategory).CurrentValues.SetValues(categoryDto);
        await context.SaveChangesAsync();    
    }

    public async Task DeleteAsync(ResolutionCategory category)
    {
        context.ResolutionCategories.Remove(category);
        await context.SaveChangesAsync();
    }
}