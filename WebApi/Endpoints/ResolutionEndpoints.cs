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
        app.MapGet("resolutions/{resolutionId}", FindByIdAsync);
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

    // IN PROGRESS
    public static async Task<IResult> AddAsync(ResolutionDto resolutionDto,
        IMapper mapper,
        IResolutionRepository resolutionRepository)
    {
        var resolution = mapper.Map<Resolution>(resolutionDto);
        await resolutionRepository.AddAsync(resolution);
        return TypedResults.CreatedAtRoute($"Resolutions/{resolution.Id}", resolution);
    }
}