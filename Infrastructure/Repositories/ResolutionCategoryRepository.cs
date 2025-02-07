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

    public Task UpdateAsync(ResolutionCategory category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ResolutionCategory category)
    {
        throw new NotImplementedException();
    }
}