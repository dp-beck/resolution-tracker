using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Infrastructure.Repositories;

public class ResolutionRepository (ResolutionDbContext context, IResolutionCategoryRepository categoryRepository) : IResolutionRepository
{
    public async Task<IEnumerable<Resolution>> GetAllAsync()
    {
        return await context.Resolutions
            .Include(c => c.Category)
            .ToListAsync();
    }

    public async Task<Resolution?> FindByIdAsync(int id)
    {
        return await context.Resolutions
                .Include(c => c.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Resolution resolution)
    { 
        await context.Resolutions.AddAsync(resolution);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ResolutionDto resolutionDto)
    {
        var resolution = await context.Resolutions.FindAsync(id);
        if (resolution == null) return;
        
        resolution.Title = resolutionDto.Title ?? resolution.Title;
        resolution.Description = resolutionDto.Description ?? resolution.Description;
        resolution.Goal = resolutionDto.Goal ?? resolution.Goal;
        resolution.CurrentLevel = resolutionDto.CurrentLevel ?? resolution.CurrentLevel;
        resolution.IsComplete = resolutionDto.IsComplete;

        if (resolutionDto.Category != null)
        {
            var category = await categoryRepository.FindByNameAsync(resolutionDto.Category);
            resolution.Category = category;
        }
        
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Resolution resolution)
    {
        context.Resolutions.Remove(resolution);
        await context.SaveChangesAsync();
    }
}