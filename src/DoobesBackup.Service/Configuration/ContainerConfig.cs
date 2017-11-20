namespace DoobesBackup.Service.Configuration
{
    using DoobesBackup.Infrastructure;
    using DoobesBackup.Infrastructure.Repositories;
    using DoobesBackup.Service.Services;
    using SimpleInjector;
    using SimpleInjector.Extensions.ExecutionContextScoping;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ContainerConfig
    {
        public static Container GetContainer(IEnumerable<Type> additionalTypes)
        {
            // Create Simple Injector container
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            // Register application components
            container.Register<ISyncConfigurationRepository, SyncConfigurationRepository>(Lifestyle.Singleton);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
            container.Register<IAuthService, AuthService>(Lifestyle.Singleton);

            // Register types passed in
            foreach (var type in additionalTypes)
            {
                container.Register(type);
            }
            
            return container;
        }
    }
}
