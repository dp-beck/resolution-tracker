using Domain.Entities;
using Shared.Dtos;

namespace Domain.Interfaces;

public interface IResolutionCategoryRepository
{
    Task<IEnumerable<ResolutionCategory>> GetAllAsync();
    Task<ResolutionCategory?> FindByIdAsync(int id);
    Task<ResolutionCategory?> FindByNameAsync(string name);
    Task AddAsync(ResolutionCategory category);
    Task UpdateAsync(int id, ResolutionCategoryDto categoryDto);
    Task DeleteAsync(ResolutionCategory category);
}