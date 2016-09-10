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
            // TODO: consider exposing transactions outside the repository layer so the client can decide...
            using (var db = this.GetDb(true))
            {
                var result = true;
                var pm = AutoMapper.Mapper.Map<SyncConfigurationPM>(entity);
                
                // Insert the main sync config object
                result &= base.SavePM(pm);
                if (!result)
                {
                    return false; // Should be rolled back by the open transaction
                }

                // Insert the backup source
                var backupSourceRepo = new BackupSourceRepository(db);
                result &= backupSourceRepo.Save(pm.Source);
                if (!result)
                {
                    return false; // Should be rolled back by the open transaction
                }

                // Insert each destination record
                var backupDestinationRepo = new BackupDestinationRepository(db);
                foreach(var destination in pm.Destinations)
                {
                    backupDestinationRepo.Save(destination);
                }

                // Commit or rollback open transaction
                if (result)
                {
                    db.Commit();
                }
                // Else it will rollback automatically

                // Return the hydrated domain entity
                AutoMapper.Mapper.Map(pm, entity);
                
                return result;
            }
        }
    }
}
