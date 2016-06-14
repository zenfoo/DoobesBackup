//-----------------------------------------------------------------------
// <copyright file="BackupDestinationsModule.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.Modules
{
    using System.Collections.Generic;
    using Domain;
    using Nancy;
    
    /// <summary>
    /// The backup destinations api endpoint
    /// </summary>
    public class BackupDestinationsModule : NancyModule
    {
        private readonly IBackupDestinationRepository backupDestinationRepository;

        /// <summary>
        /// Initializes a new instance of the BackupDestinationsModule class
        /// </summary>
        public BackupDestinationsModule(IBackupDestinationRepository backupDestinationRepository) : base("/backupdestinations")
        {
            this.backupDestinationRepository = backupDestinationRepository;

            this.Get(
                "/", 
                args => 
                {
                    return Response.AsJson(this.backupDestinationRepository.GetAll());
                });
        }
    }
}
