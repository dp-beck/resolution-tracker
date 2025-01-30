using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;


namespace Tests.InfrastructureTests;

public class ResolutionRepositoryTests
{
    [Fact]
    public void GetAllAsync_WhenCalled_ReturnsAllResolutions()
    {
        // Arrange
        var resolutionDbContextMock = new Mock<ResolutionDbContext>();
        resolutionDbContextMock.Setup<DbSet<Resolution>?>(x => x.Resolutions)
            .ReturnsDbSet(TestDataHelper.GetFakeResolutionsList());

        var resolutionRepository = new ResolutionRepository(resolutionDbContextMock.Object);
        
        // Act
        var resolutions = resolutionRepository.GetAllAsync().Result;

        // Assert
        Assert.NotNull(resolutions);
        Assert.Equal(2, resolutions.Count());
    }
}