using Domain.Interfaces;
using Moq;

namespace Tests.Fixtures;

public class ResolutionRepositoryFixture : IDisposable
{
    public Mock<IResolutionRepository> MockRepo { get; }
    
    public ResolutionRepositoryFixture()
    {
        MockRepo = new Mock<IResolutionRepository>();
    }

    public void Dispose()
    {
    }
}