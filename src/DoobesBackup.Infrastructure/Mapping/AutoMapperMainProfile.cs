namespace DoobesBackup.Infrastructure.Mapping
{
    using AutoMapper;
    using Domain;
    using PersistenceModels;

    public class AutoMapperMainProfile : Profile
    {
        public AutoMapperMainProfile()
        {
            CreateMap<SyncConfiguration, SyncConfigurationPM>().ReverseMap();
        }
    }
}
