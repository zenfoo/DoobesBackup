namespace DoobesBackup.Infrastructure.PersistenceModels
{
    /// <summary>
    /// Persistence model for a User entity
    /// </summary>
    public class UserPM : PersistenceModel
    {
        /// <summary>
        /// The login username for this user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The value for the backup configuration item
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
