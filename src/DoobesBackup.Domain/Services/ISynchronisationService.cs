namespace DoobesBackup.Domain.Services
{
    public interface ISynchronisationService
    {
        void PerformSynchronisation(SyncConfiguration syncConfig);
    }
}
