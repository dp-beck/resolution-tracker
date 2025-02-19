using System.Runtime.CompilerServices;
using Domain.Entities;
using AutoMapper;
using Shared.Dtos;

namespace WebApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Resolution, ResolutionDto>()
            .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Name)
                );

        CreateMap<ResolutionCategory, ResolutionCategoryDto>();
    }
}
