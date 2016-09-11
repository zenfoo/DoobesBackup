namespace DoobesBackup.Infrastructure.Mapping
{
    using AutoMapper;
    using Domain;
    using PersistenceModels;

    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<SyncConfiguration, SyncConfigurationPM>()
                .AfterMap((src, targ) =>
                {
                    if (targ.Source != null)
                    {
                        targ.Source.Parent = targ;
                    }
                    
                    foreach(var dest in targ.Destinations)
                    {
                        dest.Parent = targ;
                    }
                });
            CreateMap<SyncConfigurationPM, SyncConfiguration>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .AfterMap((src, targ) =>
                {
                    // TODO: this is a tad ugly!

                    // Remove existing destinations
                    for (var ii = targ.Destinations.Count - 1; ii >= 0; ii--)
                    {
                        targ.RemoveBackupDestination(targ.Destinations[ii]);
                    }

                    // Add newly mapped
                    if (src.Destinations != null)
                    {
                        foreach (var dest in src.Destinations)
                        {
                            targ.AddBackupDestination(Mapper.Map<BackupDestination>(dest));
                        }
                    }
                });

            CreateMap<BackupSource, BackupSourcePM>()
                .ForMember(dst => dst.Parent, opt => opt.Ignore());
            CreateMap<BackupSourcePM, BackupSource>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type));

            CreateMap<BackupDestination, BackupDestinationPM>()
                .ForMember(dst => dst.Parent, opt => opt.Ignore());
            CreateMap<BackupDestinationPM, BackupDestination>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type));

            CreateMap<ConfigItem, ConfigItemPM>();
            CreateMap<ConfigItemPM, ConfigItem>();
        }
    }
}
