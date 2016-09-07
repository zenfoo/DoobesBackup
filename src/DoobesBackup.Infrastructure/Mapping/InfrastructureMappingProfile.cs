namespace DoobesBackup.Infrastructure.Mapping
{
    using AutoMapper;
    using Domain;
    using PersistenceModels;

    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<SyncConfiguration, SyncConfigurationPM>();
            CreateMap<SyncConfigurationPM, SyncConfiguration>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name));

            CreateMap<BackupSource, BackupSourcePM>();
            CreateMap<BackupSourcePM, BackupSource>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type));

            CreateMap<BackupDestination, BackupDestinationPM>();
            CreateMap<BackupDestinationPM, BackupDestination>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type));

            CreateMap<ConfigItem, ConfigItemPM>();
            CreateMap<ConfigItemPM, ConfigItem>();
        }
    }
}
