using Domain.Entities;
using Shared.Dtos;

namespace Domain.Interfaces;

public interface IResolutionRepository
{
    Task<IEnumerable<Resolution>> GetAllAsync();
    Task<Resolution?> FindByIdAsync(int id);
    Task AddAsync(Resolution resolution);
    Task UpdateAsync(int id, ResolutionDto resolutionDto);
    Task DeleteAsync(Resolution resolution);
}