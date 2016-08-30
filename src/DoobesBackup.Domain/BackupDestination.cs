//-----------------------------------------------------------------------
// <copyright file="BackupDestination.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A backup destination location
    /// </summary>
    public class BackupDestination : Entity
    {
        private Collection<ConfigItem> config;

        /// <summary>
        /// Initializes a new instance of the BackupDestination class
        /// </summary>
        /// <param name="name">The name of the backup destination</param>
        /// <param name="type">The type of the backup destination</param>
        public BackupDestination(string name, string type)
        {
            this.Name = name;
            this.Type = type;
            this.config = new Collection<ConfigItem>();
        }

        /// <summary>
        /// Gets the name of the backup destination
        /// </summary>
        public virtual string Name { get; private set; }

        public virtual string Type { get; private set; }
        
        public void AddConfigItem(ConfigItem configItem)
        {
            this.config.Add(configItem);
        }

        public void RemoveConfigItem(ConfigItem configItem)
        {
            this.config.Remove(configItem);
        }

        public virtual ReadOnlyCollection<ConfigItem> Config
        {
            get
            {
                return new ReadOnlyCollection<ConfigItem>(this.config);
            }
        }
    }
}
