//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationRepository.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.Repositories
{
    using System;
    using Dapper;
    using DoobesBackup.Domain;
    using PersistenceModels;
    using System.Collections.Generic;

    /// <summary>
    /// Implementation of the SyncConfiguration repository
    /// </summary>
    public class SyncConfigurationRepository : Repository<SyncConfiguration, SyncConfigurationPM>, ISyncConfigurationRepository
    {
        public SyncConfigurationRepository() : base("SyncConfigurations") { }
        
        public override bool Save(SyncConfiguration entity)
        {
            using (var db = this.GetDb(true))
            {
                var result = base.Save(entity);

                // Insert the backup source
                var backupSourceRepo = new BackupSourceRepository(db);
                backupSourceRepo.Save(entity.Source);

                // Insert each destination record
                var backupDestinationRepo = new BackupDestinationRepository(db);
                foreach(var destination in entity.Destinations)
                {
                    backupDestinationRepo.Save(destination);
                }

                // Commit or rollback transaction
                if (result)
                {
                    db.Commit();
                }
                else
                {
                    // It will rollback automatically
                }

                return result;
            }
        }
    }
}
