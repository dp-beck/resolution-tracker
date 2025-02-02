using AutoMapper;
using Domain.Interfaces;
using WebApi.Dtos;

namespace WebApi.Endpoints;

public static class ResolutionEndpoints
{
    public static async Task<IResult> GetAllAsync(IMapper mapper,
        IResolutionRepository resolutionRepository)
    {
        var resolutions = await resolutionRepository.GetAllAsync();
        var resolutionDtos = mapper.Map<List<ResolutionDto>>(resolutions);
        return TypedResults.Ok(resolutionDtos);
    }

    public static async Task<IResult> FindByIdAsync(IMapper mapper,
        IResolutionRepository resolutionRepository,
        int resolutionId)
    {
        var resolution = await resolutionRepository.FindByIdAsync(resolutionId);
        var resolutionDto = mapper.Map<ResolutionDto>(resolution);
        return TypedResults.Ok(resolutionDto);
    }
}