//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service
{
    using DoobesBackup.Service.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Nancy.Owin;

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
        }

        /// <summary>
        /// Configure the application
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The current hosting environment details</param>
        /// <param name="loggerFactory">The logger factory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            var config = this.Configuration;
            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(config, appConfig);

            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new NancyBootstrapper(appConfig)));
        }
    }
}
