namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupDestinationRepository : Repository<BackupDestination, BackupDestinationPM>
    {
        public BackupDestinationRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupDestinations", dbConnectionWrapper) { }
    }
}
