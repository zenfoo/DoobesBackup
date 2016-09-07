namespace DoobesBackup.Infrastructure
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.Repositories;

    public interface ISyncConfigurationRepository : IRepository<SyncConfiguration>
    {
    }
}
