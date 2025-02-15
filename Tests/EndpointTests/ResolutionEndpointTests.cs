using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.Fixtures;
using WebApi.Dtos;
using WebApi.Endpoints;

namespace Tests.EndpointTests;

public class ResolutionEndpointTests : IClassFixture<ResolutionRepositoryFixture>,
    IClassFixture<ResolutionCategoryRepositoryFixture>, 
    IClassFixture<MapperFixture>
{
    private readonly ResolutionRepositoryFixture _resolutionRepositoryFixture;
    private readonly ResolutionCategoryRepositoryFixture _resolutionCategoryRepositoryFixture;
    private readonly MapperFixture _mapperFixture;

    public ResolutionEndpointTests(ResolutionRepositoryFixture resolutionRepositoryFixture, 
        MapperFixture mapperFixture,
        ResolutionCategoryRepositoryFixture resolutionCategoryRepositoryFixture)
    {
        _resolutionRepositoryFixture = resolutionRepositoryFixture;
        _resolutionCategoryRepositoryFixture = resolutionCategoryRepositoryFixture;
        _mapperFixture = mapperFixture;
    }
    
    [Fact]
    public async Task GetAllAsync_WhenCalled_ReturnsAllResolutions()
    {
        // Arrange
        _resolutionRepositoryFixture.MockRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(TestDataHelper.GetFakeResolutionsList());
        
        // Act
        var returnValue = await ResolutionEndpoints.GetAllAsync(
            _mapperFixture.Mapper, 
            _resolutionRepositoryFixture.MockRepo.Object);
        var result = returnValue as Ok<List<ResolutionDto>>;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        
        Assert.NotNull(result.Value);
        Assert.IsType<List<ResolutionDto>>(result.Value);
        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public async Task FindByIdAsync_WhenCalled_ReturnsResolution()
    {
        // Arrange
        _resolutionRepositoryFixture.MockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestDataHelper.GetFakeResolution());
        
        // Act
        var returnValue = await ResolutionEndpoints.FindByIdAsync(1, 
            _mapperFixture.Mapper, 
            _resolutionRepositoryFixture.MockRepo.Object);
        var result = returnValue as Ok<ResolutionDto>;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        
        Assert.NotNull(result.Value);
        Assert.IsType<ResolutionDto>(result.Value);
        Assert.Equal("First Resolution", result.Value.Title);
    }

    [Fact]
    public async Task AddAsync_WhenCalled_AddsResolution()
    {
        // Arrange
        ResolutionDto resolutionDto = new ResolutionDto
        {
            Title = "New Resolution",
            Description = "New Description",
            Goal = 23,
            Category = "Hobbies"
        };
        
        _resolutionRepositoryFixture.MockRepo.Setup(repo => 
                repo.AddAsync(It.IsAny<Resolution>()));
        
        // Act
        var returnValue = await ResolutionEndpoints.AddAsync(resolutionDto, 
            _resolutionRepositoryFixture.MockRepo.Object,
            _resolutionCategoryRepositoryFixture.MockRepo.Object
            );
        
        var result = returnValue as CreatedAtRoute<ResolutionDto>;
        // Assert
        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.NotNull(result.Value);
        Assert.IsType<ResolutionDto>(result.Value);
        Assert.Equal("New Resolution", result.Value.Title);
    }

    [Fact]
    public async Task UpdateAsync_WhenCalled_UpdatesResolution()
    {
        // Arrange
        ResolutionDto resolutionDto = new ResolutionDto
        {
            Title = "Updated Resolution",
        };
        
        // Act
        var returnValue = await ResolutionEndpoints.UpdateAsync(1, 
            resolutionDto, 
            _resolutionRepositoryFixture.MockRepo.Object,
            _resolutionCategoryRepositoryFixture.MockRepo.Object
        );
        
        var result = returnValue as NoContent;
        
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }
}