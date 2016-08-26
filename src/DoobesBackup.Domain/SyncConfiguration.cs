//-----------------------------------------------------------------------
// <copyright file="SyncConfiguration.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Configuration for syncing files from source to destination
    /// </summary>
    public class SyncConfiguration : Entity
    {
        public SyncConfiguration()
        {
            this.IntervalSeconds = 30 * 60; // 30 minutes by default
            this.Destinations = new List<BackupDestination>();
        }

        /// <summary>
        /// The number of seconds between sync executions
        /// </summary>
        public virtual int IntervalSeconds { get; protected set; }

        /// <summary>
        /// Gets the backup source
        /// </summary>
        public virtual BackupSource Source { get; protected set; }

        /// <summary>
        /// Gets the backup destination
        /// </summary>
        public virtual IList<BackupDestination> Destinations { get; protected set; }
        
        /// <summary>
        /// Set the backup source for this sync configuration
        /// </summary>
        /// <param name="source">The source to retrieve data from</param>
        public void SetBackupSource(BackupSource source)
        {
            this.Source = source;
        }

        /// <summary>
        /// Add the backup destination
        /// </summary>
        /// <param name="destination">The backup destination</param>
        public void AddBackupDestination(BackupDestination destination)
        {
            if (this.Destinations.Contains(destination))
            {
                throw new DomainException("Backup destination already exists in this configuration!");
            }

            this.Destinations.Add(destination);
        }

        /// <summary>
        /// Remove the backup destination
        /// </summary>
        /// <param name="destination">The backup destination</param>
        public void RemoveBackupDestination(BackupDestination destination)
        {
            if (!this.Destinations.Contains(destination))
            {
                throw new DomainException("The backup destination does not exist in this configuration");
            }

            this.Destinations.Remove(destination);
        }
        
        /// <summary>
        /// Peform the backup operation
        /// </summary>
        public void PerformBackup()
        {

            // List the files on the source

            // List the files on the destination

            // Copy files from source to destination that are new

            // 


            throw new NotImplementedException();
        }
    }
}
