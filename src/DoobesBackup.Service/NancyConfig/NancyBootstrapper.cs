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

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            // Enable CORS
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-Type");

            });
        }
    }
}
