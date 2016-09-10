namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupDestinationRepository : PersistenceRepository<BackupDestinationPM>
    {
        public BackupDestinationRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupDestinations", dbConnectionWrapper) { }
    }
}
