//-----------------------------------------------------------------------
// <copyright file="BackupSourcePM.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.PersistenceModels
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// A backup source location
    /// </summary>
    public class BackupSourcePM : PersistenceModel
    {
        public BackupSourcePM()
        {
            this.Config = new Collection<SourceConfigItemPM>();
        }

        /// <summary>
        /// The parent sync configuration
        /// </summary>
        public SyncConfigurationPM Parent { get; set; }

        /// <summary>
        /// Gets or sets the name for the backup source
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The type name of the backup source
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The associated configuration for this backup source
        /// </summary>
        public Collection<SourceConfigItemPM> Config { get; set; }
    }
}
