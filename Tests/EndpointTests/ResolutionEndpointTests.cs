using AutoMapper;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tests.Fixtures;
using WebApi;
using WebApi.Dtos;
using WebApi.Endpoints;

namespace Tests.EndpointTests;

public class ResolutionEndpointTests : IClassFixture<ResolutionRepositoryFixture>, IClassFixture<MapperFixture>
{
    private readonly ResolutionRepositoryFixture _resolutionRepositoryFixture;
    private readonly MapperFixture _mapperFixture;

    public ResolutionEndpointTests(ResolutionRepositoryFixture resolutionRepositoryFixture, MapperFixture mapperFixture)
    {
        _resolutionRepositoryFixture = resolutionRepositoryFixture;
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
}