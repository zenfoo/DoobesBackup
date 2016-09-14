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
                    foreach (var dest in src.Destinations)
                    {
                        targ.AddBackupDestination(Mapper.Map<BackupDestination>(dest));
                    }
                });

            CreateMap<BackupSource, BackupSourcePM>()
                .ForMember(dst => dst.Parent, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    foreach (var cfg in dest.Config)
                    {
                        cfg.Parent = dest;
                    }
                    //dest.Config.Clear();

                    //if (src.Config != null)
                    //{
                    //    foreach (var cfg in src.Config)
                    //    {
                    //        dest.Config.Add(Mapper.Map<SourceConfigItemPM>(cfg));
                    //    }
                    //}
                });
            CreateMap<BackupSourcePM, BackupSource>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type))
                .AfterMap((src, dest) =>
                {
                    for(var ii = dest.Config.Count - 1; ii >= 0; ii--)
                    {
                        dest.RemoveConfigItem(dest.Config[ii]);
                    }

                    foreach (var cfg in src.Config)
                    {
                        dest.AddConfigItem(Mapper.Map<ConfigItem>(cfg));
                    }
                });

            CreateMap<BackupDestination, BackupDestinationPM>()
                .ForMember(dst => dst.Parent, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    foreach (var cfg in dest.Config)
                    {
                        cfg.Parent = dest;
                    }

                    //dest.Config.Clear();

                    //if (src.Config != null)
                    //{
                    //    foreach (var cfg in src.Config)
                    //    {

                    //        dest.Config.Add(Mapper.Map<SourceConfigItemPM>(cfg));
                    //    }
                    //}
                });
            CreateMap<BackupDestinationPM, BackupDestination>()
                .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("type", opt => opt.MapFrom(src => src.Type))
                .AfterMap((src, dest) =>
                {
                    for (var ii = dest.Config.Count - 1; ii >= 0; ii--)
                    {
                        dest.RemoveConfigItem(dest.Config[ii]);
                    }

                    foreach (var cfg in src.Config)
                    {
                        dest.AddConfigItem(Mapper.Map<ConfigItem>(cfg));
                    }
                });

            CreateMap<ConfigItem, SourceConfigItemPM>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore());
            CreateMap<SourceConfigItemPM, ConfigItem>();

            CreateMap<ConfigItem, DestinationConfigItemPM>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore()); ;
            CreateMap<DestinationConfigItemPM, ConfigItem>();
        }
    }
}
