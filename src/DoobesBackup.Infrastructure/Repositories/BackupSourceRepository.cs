namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupSourceRepository : PersistenceRepository<BackupSourcePM>
    {
        public BackupSourceRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupSources", dbConnectionWrapper) { }
    }
}
