//-----------------------------------------------------------------------
// <copyright file="NancyBootstrapper.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service
{
    using System;
    using DoobesBackup.Service.Configuration;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Configuration;

    /// <summary>
    /// Bootstrap the nancy framework
    /// </summary>
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NancyBootstrapper"/> class
        /// </summary>
        /// <param name="appConfig">The application configuration</param>
        public NancyBootstrapper(AppConfiguration appConfig)
        {
            Console.WriteLine("Bootstrapping Nancy...");
            Console.WriteLine(string.Format("Using smtp server: {0}:{1}", appConfig.Smtp.Server, appConfig.Smtp.Port.ToString()));
        }
    }
}
