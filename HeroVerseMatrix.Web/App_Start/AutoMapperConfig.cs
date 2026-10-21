using AutoMapper;
using HeroVerseMatrix.Application.Mappings;

public static class AutoMapperConfig
{
    public static IMapper Mapper { get; private set; }

    public static void Register()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ApplicationMappingProfile>();
        }, null);

        configuration.AssertConfigurationIsValid();
        Mapper = configuration.CreateMapper();
    }
}