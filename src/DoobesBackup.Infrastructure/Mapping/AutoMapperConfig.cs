namespace DoobesBackup.Infrastructure.Mapping
{
    using AutoMapper;
    using Framework;

    public class AutoMapperConfig : IGlobalConfiguration
    {
        public void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AutoMapperMainProfile>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
