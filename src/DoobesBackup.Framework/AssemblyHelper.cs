namespace DoobesBackup.Framework
{
    using Microsoft.Extensions.DependencyModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class AssemblyHelper
    {
        public static string GetAssemblyPathForType(Type containedType)
        {
            return containedType.GetTypeInfo().Assembly.Location;
        }

        public static string GetAssemblyPath(string assemblyName)
        {
            var libraries = DependencyContext.Default.RuntimeLibraries;
            var assemblies = libraries.SelectMany(l => l.GetDefaultAssemblyNames(DependencyContext.Default));
            var assembly = assemblies.Where(assName => assName.Name == assemblyName || assName.FullName == assemblyName).Select(Assembly.Load).FirstOrDefault();
            if (assembly != null)
            {
                return assembly.Location;
            }

            return null;
        }

        public static IEnumerable<Assembly> GetAllDependantAssemblies()
        {
            var libraries = DependencyContext.Default.RuntimeLibraries;
            var assemblies = libraries.SelectMany(s => s.GetDefaultAssemblyNames(DependencyContext.Default)).Select(Assembly.Load);
            return assemblies;
        }
    }
}
