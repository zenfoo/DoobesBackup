namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupDestinationConfigRepository : PersistenceRepository<SourceConfigItemPM>
    {
        public BackupDestinationConfigRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupDestinationConfigItems", dbConnectionWrapper) { }
    }
}
