//-----------------------------------------------------------------------
// <copyright file="SyncConfiguration.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Configuration for syncing files from source to destination
    /// </summary>
    public class SyncConfiguration : Entity
    {
        private Collection<BackupDestination> destinations;

        public SyncConfiguration(string name) : this(name, 30 * 60) { }

        public SyncConfiguration(string name, int intervalSeconds)
        {
            this.Name = name;
            this.IntervalSeconds = intervalSeconds;
            this.Source = null;
            this.destinations = new Collection<BackupDestination>();
        }

        public virtual string Name { get; private set; }

        /// <summary>
        /// The number of seconds between sync executions
        /// </summary>
        public virtual int IntervalSeconds { get; protected set; }

        /// <summary>
        /// Gets the backup source
        /// </summary>
        public virtual BackupSource Source { get; protected set; }

        /// <summary>
        /// Gets the backup destinations
        /// </summary>
        public ReadOnlyCollection<BackupDestination> Destinations
        {
            get
            {
                return new ReadOnlyCollection<BackupDestination>(this.destinations);
            }
        }
        
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
            if (this.destinations.Contains(destination))
            {
                throw new DomainException("Backup destination already exists in this configuration!");
            }

            this.destinations.Add(destination);
        }

        /// <summary>
        /// Remove the backup destination
        /// </summary>
        /// <param name="destination">The backup destination</param>
        public void RemoveBackupDestination(BackupDestination destination)
        {
            if (!this.destinations.Contains(destination))
            {
                throw new DomainException("The backup destination does not exist in this configuration");
            }

            this.destinations.Remove(destination);
        }
        
        /// <summary>
        /// Analyse the backup operations required
        /// TODO: this may need to be moved out to a domain service
        /// </summary>
        public ReadOnlyCollection<SyncAction> GetSyncActions()
        {
            // List the files on the source

            // List the files on the destination

            // Determine the correct action to take for each file on the source (copy, update or leave)

            // Determine the correct action to take for each file on the destination (update, delete or leave)


            throw new NotImplementedException();
        }
    }
}
