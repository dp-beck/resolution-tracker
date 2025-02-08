using Domain.Interfaces;
using Moq;

namespace Tests.Fixtures;

public class ResolutionCategoryRepositoryFixture : IDisposable
{
    public Mock<IResolutionCategoryRepository> MockRepo { get; }
    
    public ResolutionCategoryRepositoryFixture()
    {
        MockRepo = new Mock<IResolutionCategoryRepository>();
    }

    public void Dispose()
    {
    }
}