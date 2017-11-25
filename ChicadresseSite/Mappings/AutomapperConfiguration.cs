using AutoMapper;

namespace ChicadresseSite.Mappings
{
    public class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainToViewModelMappingProfile>();
                config.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}