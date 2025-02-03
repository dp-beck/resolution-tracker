using AutoMapper;
using Domain.Interfaces;
using WebApi.Dtos;

namespace WebApi.Endpoints;

public static class ResolutionEndpoints 
{
    public static void RegisterResolutionEndpoints(this WebApplication app)
    {
        app.MapGet("/resolutions", GetAllAsync);
        app.MapGet("resolutions/{resolutionId}", FindByIdAsync);    
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
}