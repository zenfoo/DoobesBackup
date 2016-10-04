namespace DoobesBackup.Infrastructure.Repositories
{
    using System;
    using DoobesBackup.Domain;
    using DoobesBackup.Infrastructure.PersistenceModels;

    public class BackupSourceRepository : PersistenceRepository<BackupSourcePM>
    {
        public BackupSourceRepository(DbConnectionWrapper dbConnectionWrapper) : base("BackupSources", dbConnectionWrapper) { }

        public override bool Save(BackupSourcePM pm)
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
                var configRepo = new BackupSourceConfigRepository(db);
                foreach(var configItem in pm.Config)
                {
                    result &= configRepo.Save(configItem);
                }

                if (result)
                {
                    db.Commit();
                }

                return result;
            }
        }
    }
}
