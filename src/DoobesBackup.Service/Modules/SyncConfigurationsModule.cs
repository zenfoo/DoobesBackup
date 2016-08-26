//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationsModule.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.Modules
{
    using System.Collections.Generic;
    using Domain;
    using Nancy;
    using Infrastructure;

    /// <summary>
    /// The sync configuration api endpoint
    /// </summary>
    public class SyncConfigurationsModule : NancyModule
    {
        private readonly ISyncConfigurationRepository syncConfigurationRepository;

        /// <summary>
        /// Initializes a new instance of the SyncConfigurationsModule class
        /// </summary>
        public SyncConfigurationsModule(ISyncConfigurationRepository syncConfigurationRepository) : base("/syncconfigurations")
        {
            this.syncConfigurationRepository = syncConfigurationRepository;

            this.Get(
                "/", 
                args => 
                {
                    return Response.AsJson(this.syncConfigurationRepository.GetAll());
                });
        }
    }
}
