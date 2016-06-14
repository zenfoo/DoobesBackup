//-----------------------------------------------------------------------
// <copyright file="BackupSource.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;

    /// <summary>
    /// A backup source location
    /// </summary>
    public class BackupSource : IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the BackupSource class
        /// </summary>
        /// <param name="name">The name of the backup source</param>
        public BackupSource(Guid? id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid? Id { get; private set; }

        /// <summary>
        /// Gets the name for the backup source
        /// </summary>
        public string Name { get; private set; }
    }
}
