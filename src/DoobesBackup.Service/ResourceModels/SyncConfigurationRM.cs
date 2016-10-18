namespace DoobesBackup.Service.ResourceModels
{
    using System.Collections.ObjectModel;

    public class SyncConfigurationRM : ResourceModel
    {
        public SyncConfigurationRM()
        {
            this.Destinations = new Collection<BackupDestinationRM>();
        }

        public int IntervalSeconds { get; set; }
        public string Name { get; set; }
        public BackupSourceRM Source { get; set; }
        public Collection<BackupDestinationRM> Destinations { get; set; }
    }
}
