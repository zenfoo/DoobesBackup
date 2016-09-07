//-----------------------------------------------------------------------
// <copyright file="NancyBootstrapper.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.NancyConfig
{
    using System;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;
    using SimpleInjector;
    using Nancy.Validation;
    using SimpleInjector.Extensions.ExecutionContextScoping;
    using Infrastructure;
    using Infrastructure.Repositories;

    /// <summary>
    /// Bootstrap the nancy framework
    /// </summary>
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        ////private readonly IServiceCollection services;
        //private readonly IServiceProvider serviceProvider;
        //private readonly AppConfiguration appConfig;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="NancyBootstrapper"/> class
        ///// </summary>
        ///// <param name="appConfig">The application configuration</param>
        //public NancyBootstrapper(
        //    AppConfiguration appConfig, 
        //    /*IServiceCollection services, */
        //    IServiceProvider serviceProvider)
        //{
        //    //this.services = services;
        //    this.serviceProvider = serviceProvider;
        //    this.appConfig = appConfig;
            
            
        //}

        protected override void ApplicationStartup(TinyIoCContainer nancy, IPipelines pipelines)
        {
            Console.WriteLine("Bootstrapping Nancy...");
            //Console.WriteLine(string.Format("Using smtp server: {0}:{1}", appConfig.Smtp.Server, appConfig.Smtp.Port.ToString()));

            // Create Simple Injector container
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            // Register application components
            container.Register<ISyncConfigurationRepository, SyncConfigurationRepository>(Lifestyle.Scoped);

            // Register Nancy modules.
            foreach (var nancyModule in this.Modules)
            {
                container.Register(nancyModule.ModuleType);
            }

            // Cross-wire Nancy abstractions that application components require (if any). e.g.:
            //container.Register(nancy.Resolve<IModelValidator>);

            // Check the container.
            container.Verify();

            // Hook up Simple Injector in the Nancy pipeline.
            nancy.Register(typeof(INancyModuleCatalog), new SimpleInjectorModuleCatalog(container));
            nancy.Register(
                typeof(INancyContextFactory), 
                new SimpleInjectorScopedContextFactory(
                    container, 
                    nancy.Resolve<INancyContextFactory>()));
        }

        //protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        //{
        //    base.ConfigureApplicationContainer(container);

        //    // Register all the service collection types with application scope
        //    foreach (var serviceDescriptor in services)
        //    {
        //        switch (serviceDescriptor.Lifetime)
        //        {
        //            case ServiceLifetime.Singleton:
        //                // Appication scoped singleton
        //                this.RegisterSingleton(serviceDescriptor, container);
        //                break;

        //            case ServiceLifetime.Transient:
        //                // Application transient object
        //                this.RegisterTransient(serviceDescriptor, container);
        //                break;
        //        }
        //    }
        //}


        //protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        //{
        //    base.ConfigureRequestContainer(container, context);

        //    // Register all the service collection types with request scope
        //    foreach (var serviceDescriptor in services)
        //    {
        //        switch (serviceDescriptor.Lifetime)
        //        {
        //            case ServiceLifetime.Scoped:
        //                // Request scoped singleton - for the lifetime of the request
        //                this.RegisterSingleton(serviceDescriptor, container);
        //                break;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Register the type, instance or factory method with TinyIoC as a singleton
        ///// </summary>
        ///// <param name="serviceDescriptor">The service descriptor</param>
        ///// <param name="container">The TinyIoC container</param>
        ///// <returns>Registration options which can be modified</returns>
        //private TinyIoCContainer.RegisterOptions RegisterSingleton(ServiceDescriptor serviceDescriptor, TinyIoCContainer container)
        //{
        //    if (serviceDescriptor.ImplementationType != null)
        //    {
        //        // Register a type
        //        return container.Register(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType).AsSingleton();
        //    }
        //    else if (serviceDescriptor.ImplementationInstance != null)
        //    {
        //        // Register an instance
        //        return container.Register(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationInstance);
        //    }
        //    else if (serviceDescriptor.ImplementationFactory != null)
        //    {
        //        // Register using the factory method
        //        return container.Register(serviceDescriptor.ServiceType, (tinyIoC, p) =>
        //        {
        //            return serviceDescriptor.ImplementationFactory(this.serviceProvider);
        //        }).AsSingleton();
        //    }

        //    throw new ConfigurationException("Invalid service descriptor encountered");
        //}

        ///// <summary>
        ///// Register the type, instance or factory method with TinyIoC as multi-instance
        ///// </summary>
        ///// <param name="serviceDescriptor">The service descriptor</param>
        ///// <param name="container">The TinyIoC container</param>
        ///// <returns>Registration options which can be modified</returns>
        //private TinyIoCContainer.RegisterOptions RegisterTransient(ServiceDescriptor serviceDescriptor, TinyIoCContainer container)
        //{
        //    if (serviceDescriptor.ImplementationType != null)
        //    {
        //        // Register a type
        //        return container.Register(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType).AsMultiInstance();

        //    } else if (serviceDescriptor.ImplementationFactory != null) {
        //        // Register using the factory method
        //        return container.Register(serviceDescriptor.ServiceType, (tinyIoC, p) =>
        //        {
        //            return serviceDescriptor.ImplementationFactory(this.serviceProvider);
        //        }).AsMultiInstance();
        //    }

        //    throw new ConfigurationException("Invalid service descriptor encountered");
        //}
    }
}
