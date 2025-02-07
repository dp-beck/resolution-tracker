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
        IMapper mapper,
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
        return TypedResults.CreatedAtRoute("FindByIdAsync", new { resolutionId = resolution.Id });
    }
}