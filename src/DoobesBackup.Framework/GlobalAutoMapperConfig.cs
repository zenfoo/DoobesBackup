namespace DoobesBackup.Framework
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyModel;
    using System.Linq;
    using System.Reflection;

    public class GlobalAutoMapperConfig : IGlobalConfiguration
    {
        public void Configure()
        {
            var libraries = DependencyContext.Default.RuntimeLibraries;
            var assemblies = libraries.SelectMany(s => s.GetDefaultAssemblyNames(DependencyContext.Default)).Select(Assembly.Load);
            
            Mapper.Initialize(cfg => {
                foreach (var assembly in assemblies)
                {
                    var profileTypes = assembly.GetTypes()
                        .Where(t => typeof(Profile).IsAssignableFrom(t))
                        .Where(t => !t.GetTypeInfo().IsAbstract)
                        .Where(t => !t.GetTypeInfo().IsInterface)
                        .Where(t => t.GetTypeInfo().IsClass)
                        .Where(t => t.GetConstructors().Any(c => c.GetParameters().Count() == 0));
                    foreach (var profileType in profileTypes)
                    {
                        cfg.AddProfile(profileType);
                    }
                }
            });
            
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
