using AutoMapper;
using WebApi;

namespace Tests.Fixtures;

public class MapperFixture : IDisposable
{
    public MappingProfile MappingProfile { get; }
    private MapperConfiguration Configuration { get; }
    public Mapper Mapper { get; }
    
    public MapperFixture()
    {
        MappingProfile = new MappingProfile();
        Configuration = new MapperConfiguration(cfg => 
            cfg.AddProfile(MappingProfile));
        Mapper  = new Mapper(Configuration);
    }

    public void Dispose()
    { }
}