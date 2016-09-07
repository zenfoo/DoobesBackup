namespace DoobesBackup.Framework
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;

    /// <summary>
    /// Discover all the IGlobalConfiguration implementations across the application
    /// </summary>
    public static class GlobalConfigurator
    {
        public static void Configure()
        {
            var libraries = DependencyContext.Default.RuntimeLibraries;
            var assemblies = libraries.SelectMany(s => s.GetDefaultAssemblyNames(DependencyContext.Default)).Select(Assembly.Load);
            
            foreach (var assembly in assemblies)
            {
                var configTypes = assembly.GetTypes()
                    .Where(t => typeof(IGlobalConfiguration).IsAssignableFrom(t))
                    .Where(t => !t.GetTypeInfo().IsAbstract)
                    .Where(t => !t.GetTypeInfo().IsInterface)
                    .Where(t => t.GetTypeInfo().IsClass);
                foreach (var configType in configTypes)
                {
                    var configuration = Activator.CreateInstance(configType) as IGlobalConfiguration;
                    configuration.Configure();
                }
            }
        }
    }
}
