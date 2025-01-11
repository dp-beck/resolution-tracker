using Domain.Entities;

namespace Tests.DomainTests;

public class ResolutionTests
{
    [Fact]
    public void ShouldComputePercentComplete()
    {
        // Arrange
        var resolution1 = new Resolution
        {
            Goal = 101,
            CurrentLevel = 24
        };

        var result = resolution1.PercentComplete;
        // Assert
        Assert.Equal("23.76 %", result);
    }
}