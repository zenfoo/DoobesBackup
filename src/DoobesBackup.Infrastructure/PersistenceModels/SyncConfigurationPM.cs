namespace DoobesBackup.Infrastructure.PersistenceModels
{
    using System;
    using System.Collections.ObjectModel;

    public class SyncConfigurationPM : PersistenceModel
    {
        public SyncConfigurationPM()
        {
            this.Destinations = new Collection<BackupDestinationPM>();
        }

        public virtual int IntervalSeconds { get; set; }
        public virtual string Name { get; set; }
        [Relationship(false)]
        public virtual BackupSourcePM Source { get; set; }
        public virtual Collection<BackupDestinationPM> Destinations { get; set; }
    }
}
