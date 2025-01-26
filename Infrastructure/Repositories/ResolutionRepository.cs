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

    public Task<Resolution?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Resolution resolution)
    {
        throw new NotImplementedException();
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