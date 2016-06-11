//-----------------------------------------------------------------------
// <copyright file="BackupSource.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    /// <summary>
    /// A backup source location
    /// </summary>
    public class BackupSource
    {
        /// <summary>
        /// Initializes a new instance of the BackupSource class
        /// </summary>
        /// <param name="name">The name of the backup source</param>
        public BackupSource(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name for the backup source
        /// </summary>
        public string Name { get; private set; }
    }
}
