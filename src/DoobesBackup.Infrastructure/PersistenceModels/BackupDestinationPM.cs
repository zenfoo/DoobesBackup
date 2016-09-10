//-----------------------------------------------------------------------
// <copyright file="BackupDestinationPM.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.PersistenceModels
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// A backup destination location
    /// </summary>
    public class BackupDestinationPM : PersistenceModel
    {
        /// <summary>
        /// The parent sync configuration
        /// </summary>
        public SyncConfigurationPM Parent { get; set; }

        /// <summary>
        /// Gets or sets the name for the backup destination
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The type name of the backup destination
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The associated configuration for this backup destination
        /// </summary>
        public Collection<ConfigItemPM> Config { get; set; }
    }
}
