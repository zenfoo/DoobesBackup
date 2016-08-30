//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service
{
    using Domain;
    using DoobesBackup.Service.Configuration;
    using Framework;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Nancy.Owin;
    using System;

    /// <summary>
    /// The startup class for the service
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the Startup class
        /// </summary>
        /// <param name="env">The current hosting environment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                              .AddEnvironmentVariables()
                              .SetBasePath(env.ContentRootPath);

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the current application configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Configure application services for dependency injection
        /// </summary>
        /// <param name="services">The current services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISyncConfigurationRepository, SyncConfigurationRepository>();
            services.AddSingleton(services);
        }

        /// <summary>
        /// Configure the application
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The current hosting environment details</param>
        /// <param name="loggerFactory">The logger factory</param>
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory, 
            ISyncConfigurationRepository syncConfigurationRepository,
            IServiceCollection services)
        {
            // Initialise logging
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            // Setup database configuration
            DatabaseInitializer.Initialize();

            // Setup any other global configuration objects that implement IGlobalConfiguration
            GlobalConfigurator.Configure();


            ////
            //// DEMO - Create a dummy configuration
            ////
            //syncConfigurationRepository.Create(new SyncConfiguration(null, 60, new BackupSource(null, "Synology NAS"), new BackupDestination(null, "AWS S3")));
            
            // Specify error message display
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            // Map application configuration
            var config = this.Configuration;
            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(config, appConfig);

            // Wire up nancy
            app.UseOwin(x => 
                x.UseNancy(opt => 
                    opt.Bootstrapper = new NancyBootstrapper(appConfig, services, app.ApplicationServices)));
        }
    }
}
