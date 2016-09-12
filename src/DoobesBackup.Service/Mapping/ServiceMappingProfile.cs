namespace DoobesBackup.Service.Mapping
{
    using AutoMapper;
    using Domain;
    using ResourceModels;

    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<SyncConfiguration, SyncConfigurationRM>();
            CreateMap<SyncConfigurationRM, SyncConfiguration>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .AfterMap((src, dest) =>
                {
                    foreach (var bkpDest in src.Destinations)
                    {
                        dest.AddBackupDestination(Mapper.Map<BackupDestination>(bkpDest));
                    }
                });
            
            CreateMap<BackupSource, BackupSourceRM>()
                .AfterMap((src, dest) =>
                {
                    dest.Config.Clear();

                    foreach(var config in src.Config)
                    {
                        dest.Config.Add(Mapper.Map<ConfigItemRM>(config));
                    }
                });
            CreateMap<BackupSourceRM, BackupSource>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type))
                .AfterMap((src, dest) =>
                {
                    // Remove existing config items
                    // TOOD: this is an issue because we're losing the id's if they're already persisted? Maybe..
                    for (var ii = dest.Config.Count - 1; ii >= 0; ii--)
                    {
                        dest.RemoveConfigItem(dest.Config[ii]);
                    }

                    // Add in new ones
                    foreach(var config in src.Config)
                    {
                        dest.AddConfigItem(Mapper.Map<ConfigItem>(config));
                    }
                });

            CreateMap<BackupDestination, BackupDestinationRM>()
                .AfterMap((src, dest) =>
                {
                    dest.Config.Clear();

                    foreach (var config in src.Config)
                    {
                        dest.Config.Add(Mapper.Map<ConfigItemRM>(config));
                    }
                }); ;
            CreateMap<BackupDestinationRM, BackupDestination>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type))
                .AfterMap((src, dest) =>
                {
                    // Remove existing config items
                    // TOOD: this is an issue because we're losing the id's if they're already persisted? Maybe..
                    for (var ii = dest.Config.Count - 1; ii >= 0; ii--)
                    {
                        dest.RemoveConfigItem(dest.Config[ii]);
                    }

                    // Add in new ones
                    foreach (var config in src.Config)
                    {
                        dest.AddConfigItem(Mapper.Map<ConfigItem>(config));
                    }
                });

            CreateMap<ConfigItem, ConfigItemRM>();
            CreateMap<ConfigItemRM, ConfigItem>();

        }
    }
}
