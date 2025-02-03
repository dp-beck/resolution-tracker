using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResolutionRepository (ResolutionDbContext context) : IResolutionRepository
{
    public async Task<IEnumerable<Resolution>> GetAllAsync()
    {
        return await context.Resolutions
            .Include(c => c.Category)
            .ToListAsync();
    }

    public async Task<Resolution?> FindByIdAsync(int id)
    {
        return await context.Resolutions.FindAsync(id);
    }

    public async Task AddAsync(Resolution resolution)
    { 
        await context.Resolutions.AddAsync(resolution);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(Resolution resolution)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Resolution resolution)
    {
        throw new NotImplementedException();
    }
}