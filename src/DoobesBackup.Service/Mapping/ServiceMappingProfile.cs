﻿namespace DoobesBackup.Service.Mapping
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
