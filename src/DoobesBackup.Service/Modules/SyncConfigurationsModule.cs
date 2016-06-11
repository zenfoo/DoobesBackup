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
    
    /// <summary>
    /// The sync configuration api endpoint
    /// </summary>
    public class SyncConfigurationsModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the SyncConfigurationsModule class
        /// </summary>
        public SyncConfigurationsModule() : base("/syncconfigurations")
        {
            this.Get(
                "/", 
                args => 
                {
                    return Response.AsJson<IList<SyncConfiguration>>(
                        new List<SyncConfiguration>()
                        {
                            new SyncConfiguration(
                                1,
                                new BackupSource("Synology NAS"),
                                new BackupDestination("AWS S3"))
                        });
                });
        }
    }
}
