//-----------------------------------------------------------------------
// <copyright file="BackupDestination.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    /// <summary>
    /// A backup destination location
    /// </summary>
    public class BackupDestination
    {
        /// <summary>
        /// Initializes a new instance of the BackupDestination class
        /// </summary>
        /// <param name="name">The name of the backup destination</param>
        public BackupDestination(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the backup destination
        /// </summary>
        public string Name { get; private set; }
    }
}
