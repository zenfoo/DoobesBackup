namespace DoobesBackup.Infrastructure.PersistenceModels
{
    using System.Collections.ObjectModel;

    public class SyncConfigurationPM : PersistenceModel
    {
        public virtual int IntervalSeconds { get; set; }

        public virtual string Name { get; set; }

        public virtual BackupSourcePM Source { get; set; }

        public virtual Collection<BackupDestinationPM> Destinations { get; set; }
    }
}
