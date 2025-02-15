using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using WebApi.Dtos;

namespace WebApi.Endpoints;

public static class ResolutionEndpoints 
{
    public static void RegisterResolutionEndpoints(this WebApplication app)
    {
        app.MapGet("resolutions", GetAllAsync);
        app.MapGet("resolutions/{resolutionId}", FindByIdAsync).WithName("FindByIdAsync");
        app.MapPost("resolutions", AddAsync);
        app.MapPut("resolutions/{resolutionId}", UpdateAsync);
    }
    
    public static async Task<IResult> GetAllAsync(IMapper mapper,
        IResolutionRepository resolutionRepository)
    {
        var resolutions = await resolutionRepository.GetAllAsync();
        var resolutionDtos = mapper.Map<List<ResolutionDto>>(resolutions);
        return TypedResults.Ok(resolutionDtos);
    }

    public static async Task<IResult> FindByIdAsync(int resolutionId, 
        IMapper mapper,
        IResolutionRepository resolutionRepository)
    {
        var resolution = await resolutionRepository.FindByIdAsync(resolutionId);
        
        if (resolution is null) return TypedResults.NotFound();

        var resolutionDto = mapper.Map<ResolutionDto>(resolution);
        return TypedResults.Ok(resolutionDto);
    }

    public static async Task<IResult> AddAsync(ResolutionDto resolutionDto,
        IResolutionRepository resolutionRepository,
        IResolutionCategoryRepository resolutionCategoryRepository)
    {
        var category = await resolutionCategoryRepository.FindByNameAsync(resolutionDto.Category);

        Resolution resolution = new Resolution
        {
            Title = resolutionDto.Title,
            Description = resolutionDto.Description,
            Goal = resolutionDto.Goal,
            Category = category
        };
        
        await resolutionRepository.AddAsync(resolution);
        return TypedResults.CreatedAtRoute(resolutionDto,
            "FindByIdAsync", 
            new { resolutionId = resolution.Id });
    }

    // RETHINK HOW TO DO THIS
    public static async Task<IResult> UpdateAsync(int resolutionId,
        ResolutionDto resolutionDto,
        IResolutionRepository resolutionRepository,
        IResolutionCategoryRepository resolutionCategoryRepository)
    {
        var resolution = new Resolution()
        {
            Title = resolutionDto.Title,
            Description = resolutionDto.Description,
            Goal = resolutionDto.Goal,
            CurrentLevel = resolutionDto.CurrentLevel,
            CompletedOn = resolutionDto.CompletedOn,
            IsComplete = resolutionDto.IsComplete,
        };
            
        if (resolutionDto.Category is not null)
        {
            var category = await resolutionCategoryRepository.FindByNameAsync(resolutionDto.Category);
            resolution.Category = category;
        }
        
        await resolutionRepository.UpdateAsync(resolutionId, resolution);
        return TypedResults.NoContent();
    }
}