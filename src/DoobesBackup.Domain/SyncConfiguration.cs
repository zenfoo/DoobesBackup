//-----------------------------------------------------------------------
// <copyright file="SyncConfiguration.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace DoobesBackup.Domain
{
    /// <summary>
    /// Configuration for syncing files from source to destination
    /// </summary>
    public class SyncConfiguration : IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the SyncConfiguration class
        /// </summary>
        /// <param name="id">The id assigned to this sync configuration</param>
        /// <param name="intervalSeconds">The number of seconds interval between sync attempts</param>
        /// <param name="source">The backup source</param>
        /// <param name="destination">The backup destination</param>
        public SyncConfiguration(Guid? id, int intervalSeconds, BackupSource source, BackupDestination destination)
        {
            this.Id = id;
            this.IntervalSeconds = intervalSeconds;
            this.Source = source;
            this.Destination = destination;
        }

        /// <summary>
        /// Gets the configuration id
        /// </summary>
        public Guid? Id { get; private set; }

        /// <summary>
        /// The number of seconds between sync executions
        /// </summary>
        public int IntervalSeconds { get; private set; }

        /// <summary>
        /// Gets the backup source
        /// </summary>
        public BackupSource Source { get; private set; }

        /// <summary>
        /// Gets the backup destination
        /// </summary>
        public BackupDestination Destination { get; private set; }
    }
}
