namespace DoobesBackup.Service.Mapping
{
    using AutoMapper;
    using DoobesBackup.Framework;

    public class AutoMapperConfig : IGlobalConfiguration
    {
        public void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AutoMapperMainProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
