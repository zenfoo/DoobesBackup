//-----------------------------------------------------------------------
// <copyright file="BackupDestination.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;

    /// <summary>
    /// A backup destination location
    /// </summary>
    public class BackupDestination : IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the BackupDestination class
        /// </summary>
        /// <param name="name">The name of the backup destination</param>
        public BackupDestination(Guid? id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// The id of the backup destination type
        /// </summary>
        public Guid? Id { get; private set; }

        /// <summary>
        /// Gets the name of the backup destination
        /// </summary>
        public string Name { get; private set; }
    }
}
