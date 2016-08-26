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
            var assemblies = DependencyContext.Default.RuntimeLibraries.SelectMany(l => l.Assemblies).Select(ra => Assembly.Load(ra.Name));
            foreach (var assembly in assemblies)
            {
                var configTypes = assembly.GetTypes().Where(t =>
                    typeof(IGlobalConfiguration).IsAssignableFrom(t));

                foreach (var configType in configTypes)
                {
                    var configuration = Activator.CreateInstance(configType) as IGlobalConfiguration;
                    configuration.Configure();
                }
            }
        }
    }
}
