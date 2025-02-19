using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Dtos;

namespace WebApi.Endpoints;

public static class ResolutionEndpoints 
{
    public static void RegisterResolutionEndpoints(this WebApplication app)
    {
        app.MapGet("resolutions", GetAllAsync);
        app.MapGet("resolutions/{resolutionId}", FindByIdAsync).WithName("FindResolutionByIdAsync");
        app.MapPost("resolutions", AddAsync);
        app.MapPut("resolutions/{resolutionId}", UpdateAsync);
        app.MapDelete("resolutions/{resolutionId}", DeleteAsync);
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
            "FindResolutionByIdAsync", 
            new { resolutionId = resolution.Id });
    }

    public static async Task<IResult> UpdateAsync(int resolutionId,
        ResolutionDto resolutionDto,
        IResolutionRepository resolutionRepository,
        IResolutionCategoryRepository resolutionCategoryRepository)
    {
        await resolutionRepository.UpdateAsync(resolutionId, resolutionDto);
        return TypedResults.NoContent();
    }

    public static async Task<IResult> DeleteAsync(
        int resolutionId,
        IResolutionRepository resolutionRepository)
    {
        var resolution = await resolutionRepository.FindByIdAsync(resolutionId);
        if (resolution is null) return TypedResults.NotFound();
        await resolutionRepository.DeleteAsync(resolution);
        return TypedResults.NoContent();
    }
}