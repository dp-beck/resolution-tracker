using Domain.Entities;

namespace Domain.Interfaces;

public interface IResolutionCategoryRepository
{
    Task<IEnumerable<ResolutionCategory>> GetAllAsync();
    Task<ResolutionCategory?> FindByIdAsync(int id);
    Task AddAsync(ResolutionCategory category);
    Task UpdateAsync(ResolutionCategory category);
    Task DeleteAsync(ResolutionCategory category);
}