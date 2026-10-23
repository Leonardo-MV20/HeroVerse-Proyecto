using AutoMapper;
using HeroVerseMatrix.Application.Mappings;
using HeroVerseMatrix.Web.Mappings;

public static class AutoMapperConfig
{
    public static IMapper Mapper { get; private set; }

    public static void Register()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ApplicationMappingProfile>();
            config.AddProfile<WebMappingProfile>();
        });

        configuration.AssertConfigurationIsValid();
        Mapper = configuration.CreateMapper();
    }
}