using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using WebApi.Dtos;

namespace WebApi.Endpoints;

public static class ResolutionCategoryEndpoints
{
    public static void RegisterResolutionCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("resolution-categories", GetAllAsync);
        app.MapGet("resolution-categories/{resolutionCategoryId}", FindByIdAsync).WithName("FindByIdAsync");
        app.MapPost("resolution-categories", AddAsync);
        app.MapPut("resolution-categories/{resolutionId}", UpdateAsync);
        app.MapDelete("resolution-categories/{resolutionId}", DeleteAsync);
    }
    
        public static async Task<IResult> GetAllAsync(IMapper mapper,
        IResolutionCategoryRepository categoryRepository)
    {
        var categories = await categoryRepository.GetAllAsync();
        var categoryDtos = mapper.Map<List<ResolutionCategoryDto>>(categories);
        return TypedResults.Ok(categoryDtos);
    }

    public static async Task<IResult> FindByIdAsync(int resolutionCategoryId, 
        IMapper mapper,
        IResolutionCategoryRepository categoryRepository)
    {
        var resolutionCategory = await categoryRepository.FindByIdAsync(resolutionCategoryId);
        
        if (resolutionCategory is null) return TypedResults.NotFound();

        var categoryDto = mapper.Map<ResolutionCategoryDto>(resolutionCategory);
        return TypedResults.Ok(categoryDto);
    }

    public static async Task<IResult> AddAsync(ResolutionCategoryDto categoryDto,
        IResolutionCategoryRepository resolutionCategoryRepository)
    {

        ResolutionCategory category = new ResolutionCategory
        {
            Name = categoryDto.Name,
        };
        
        await resolutionCategoryRepository.AddAsync(category);
        return TypedResults.CreatedAtRoute(categoryDto,
            "FindByIdAsync", 
            new { resolutionCategoryId = category.Id });
    }

    public static async Task<IResult> UpdateAsync(int resolutionCategoryId,
        ResolutionCategoryDto categoryDto,
        IResolutionCategoryRepository resolutionCategoryRepository)
    {
        var category = new ResolutionCategory
        {
            Name = categoryDto.Name,
        };
        
        await resolutionCategoryRepository.UpdateAsync(resolutionCategoryId, category);
        return TypedResults.NoContent();
    }

    public static async Task<IResult> DeleteAsync(
        int resolutionCategoryId,
        IResolutionCategoryRepository resolutionCategoryRepository)
    {
        var category = await resolutionCategoryRepository.FindByIdAsync(resolutionCategoryId);
        if (category is null) return TypedResults.NotFound();
        await resolutionCategoryRepository.DeleteAsync(category);
        return TypedResults.NoContent();
    }
}