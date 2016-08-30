namespace DoobesBackup.Infrastructure.PersistenceModels
{
    /// <summary>
    /// A generic configuration item that can be used by a backup source or destination to store it's required configuration
    /// </summary>
    public class ConfigItemPM : PersistenceModel
    {
        /// <summary>
        /// The key name for the backup configuration item
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value for the backup configuration item
        /// </summary>
        public string Value { get; set; }
    }
}
