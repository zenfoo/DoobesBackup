//-----------------------------------------------------------------------
// <copyright file="BackupSource.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A backup source location
    /// </summary>
    public class BackupSource : Entity
    {
        private Collection<ConfigItem> config;

        /// <summary>
        /// Initializes a new instance of the BackupSource class
        /// </summary>
        /// <param name="name">The name of the backup source</param>
        /// <param name="type">The type of backup source</param>
        public BackupSource(string name, string type)
        {
            this.Name = name;
            this.Type = type;
            this.config = new Collection<ConfigItem>();
        }

        /// <summary>
        /// Gets the name for the backup source
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        /// Gets the type for the backup source
        /// </summary>
        public virtual string Type { get; private set; }

        public virtual void AddConfigItem(ConfigItem configItem)
        {
            this.config.Add(configItem);
        }
        
        public virtual void RemoveConfigItem(ConfigItem configItem)
        {
            this.config.Remove(configItem);
        }

        public ReadOnlyCollection<ConfigItem> Config {
            get
            {
                return new ReadOnlyCollection<ConfigItem>(this.config);
            }
        }
    }
}
