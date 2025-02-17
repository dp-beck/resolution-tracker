using Domain.Entities;

namespace Domain.Interfaces;

public interface IResolutionCategoryRepository
{
    Task<IEnumerable<ResolutionCategory>> GetAllAsync();
    Task<ResolutionCategory?> FindByIdAsync(int id);
    Task<ResolutionCategory?> FindByNameAsync(string name);
    Task AddAsync(ResolutionCategory category);
    Task UpdateAsync(int id, ResolutionCategory category);
    Task DeleteAsync(ResolutionCategory category);
}