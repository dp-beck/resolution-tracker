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

    public async Task UpdateAsync(int id, Resolution updatedResolution)
    {
        var oldResolution = await context.Resolutions.FindAsync(id);
        if (oldResolution != null) context.Entry(oldResolution).CurrentValues.SetValues(updatedResolution);
        await context.SaveChangesAsync();
    }

    public Task DeleteAsync(Resolution resolution)
    {
        throw new NotImplementedException();
    }
}