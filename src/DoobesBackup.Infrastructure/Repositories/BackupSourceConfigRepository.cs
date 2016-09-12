namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupSourceConfigRepository : PersistenceRepository<SourceConfigItemPM>
    {
        public BackupSourceConfigRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupSourceConfigItems", dbConnectionWrapper) { }
    }
}
