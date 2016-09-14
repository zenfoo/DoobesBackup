namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupDestinationConfigRepository : PersistenceRepository<DestinationConfigItemPM>
    {
        public BackupDestinationConfigRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupDestinationConfigItems", dbConnectionWrapper) { }
    }
}
