namespace DoobesBackup.Service.ResourceModels
{
    using System.Collections.ObjectModel;

    public class BackupSourceRM : ResourceModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Collection<ConfigItemRM> Config { get; set; }
    }
}
