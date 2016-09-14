namespace DoobesBackup.Infrastructure.Repositories
{
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupDestinationRepository : PersistenceRepository<BackupDestinationPM>
    {
        public BackupDestinationRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupDestinations", dbConnectionWrapper) { }

        public override bool Save(BackupDestinationPM pm)
        {
            // TODO: consider exposing transactions outside the repository layer so the client can decide...
            using (var db = this.GetDb(true))
            {
                var result = true;

                // Insert the main sync config object
                result &= base.Save(pm);
                if (!result)
                {
                    return false; // Should be rolled back by the open transaction
                }

                // Insert the config items
                var configRepo = new BackupDestinationConfigRepository(db);
                foreach (var configItem in pm.Config)
                {
                    result &= configRepo.Save(configItem);
                }

                return result;
            }
        }
    }
}
