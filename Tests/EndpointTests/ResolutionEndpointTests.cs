using AutoMapper;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApi;
using WebApi.Dtos;
using WebApi.Endpoints;

namespace Tests.EndpointTests;

public class ResolutionEndpointTests
{
    [Fact]
    public async Task GetAllAsync_WhenCalled_ReturnsAllResolutions()
    {
        // Arrange
        var mockRepo = new Mock<IResolutionRepository>();
        
        mockRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(TestDataHelper.GetFakeResolutionsList());

        var mappingProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
        var mapper = new Mapper(configuration);

            
        // Act
        var returnValue = await ResolutionEndpoints.GetAllAsync(mapper, mockRepo.Object);
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
        var mockRepo = new Mock<IResolutionRepository>();
        
        mockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(TestDataHelper.GetFakeResolution());
        
        var mappingProfile = new MappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
        var mapper = new Mapper(configuration);
        
        // Act
        var returnValue = await ResolutionEndpoints.FindByIdAsync(mapper, mockRepo.Object, 1);
        var result = returnValue as Ok<ResolutionDto>;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        
        Assert.NotNull(result.Value);
        Assert.IsType<ResolutionDto>(result.Value);
        Assert.Equal("First Resolution", result.Value.Title);
    }
}