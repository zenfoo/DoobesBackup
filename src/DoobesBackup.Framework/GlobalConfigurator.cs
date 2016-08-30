namespace DoobesBackup.Framework
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Discover all the IGlobalConfiguration implementations across the application
    /// </summary>
    public static class GlobalConfigurator
    {
        private static HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Microsoft.AspNetCore.Mvc",
            "Microsoft.AspNetCore.Mvc.Abstractions",
            "Microsoft.AspNetCore.Mvc.ApiExplorer",
            "Microsoft.AspNetCore.Mvc.Core",
            "Microsoft.AspNetCore.Mvc.Cors",
            "Microsoft.AspNetCore.Mvc.DataAnnotations",
            "Microsoft.AspNetCore.Mvc.Formatters.Json",
            "Microsoft.AspNetCore.Mvc.Formatters.Xml",
            "Microsoft.AspNetCore.Mvc.Localization",
            "Microsoft.AspNetCore.Mvc.Razor",
            "Microsoft.AspNetCore.Mvc.Razor.Host",
            "Microsoft.AspNetCore.Mvc.TagHelpers",
            "Microsoft.AspNetCore.Mvc.ViewFeatures"
        };

        public static void Configure()
        {

            //var entryAssembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
            //var context = DependencyContext.Load(Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName)));
            
            var libraries = DependencyContext.Default.RuntimeLibraries; //.Where(l => l.Name.StartsWith("DoobesBackup"));
            var assemblies = libraries.SelectMany(s => s.GetDefaultAssemblyNames(DependencyContext.Default)).Select(Assembly.Load);
            
            foreach (var assembly in assemblies)
            {
                var configTypes = assembly.GetTypes()
                    .Where(t => typeof(IGlobalConfiguration).IsAssignableFrom(t))
                    .Where(t => t.GetConstructors().Count() > 0); 
                foreach (var configType in configTypes)
                {
                    var configuration = Activator.CreateInstance(configType) as IGlobalConfiguration;
                    configuration.Configure();
                }
            }
        }
    }
}
