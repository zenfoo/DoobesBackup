namespace DoobesBackup.Service.Mapping
{
    using AutoMapper;
    using Domain;
    using ResourceModels;

    public class AutoMapperMainProfile : Profile
    {
        public AutoMapperMainProfile()
        {
            CreateMap<SyncConfiguration, SyncConfigurationRM>();
            CreateMap<SyncConfigurationRM, SyncConfiguration>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name));
            
            CreateMap<BackupSource, BackupSourceRM>();
            CreateMap<BackupSourceRM, BackupSource>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type));

            CreateMap<BackupDestination, BackupDestinationRM>();
            CreateMap<BackupDestinationRM, BackupDestination>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type)); 

            CreateMap<ConfigItem, ConfigItemRM>();
            CreateMap<ConfigItemRM, ConfigItem>();

        }
    }
}
