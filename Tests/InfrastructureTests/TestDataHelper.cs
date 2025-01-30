using Domain.Entities;

namespace Tests.InfrastructureTests;

public class TestDataHelper
{
    public static List<Resolution> GetFakeResolutionsList()
    {
        return
        [
            new Resolution
            {
                Id = 1,
                Title = "First Resolution"
            },

            new Resolution
            {
                Id = 2,
                Title = "Second Resolution"
            }
        ];
    }
}