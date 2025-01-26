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
}