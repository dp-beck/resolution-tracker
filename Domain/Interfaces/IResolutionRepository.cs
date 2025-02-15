using Domain.Entities;

namespace Domain.Interfaces;

public interface IResolutionRepository
{
    Task<IEnumerable<Resolution>> GetAllAsync();
    Task<Resolution?> FindByIdAsync(int id);
    Task AddAsync(Resolution resolution);
    Task UpdateAsync(int id, Resolution resolution);
    Task DeleteAsync(Resolution resolution);
}