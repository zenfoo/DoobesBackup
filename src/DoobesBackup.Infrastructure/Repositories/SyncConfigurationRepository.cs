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
    using System.Linq;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Implementation of the SyncConfiguration repository
    /// </summary>
    public class SyncConfigurationRepository : Repository<SyncConfiguration, SyncConfigurationPM>, ISyncConfigurationRepository
    {
        public SyncConfigurationRepository() : base("SyncConfigurations") { }

        public override SyncConfiguration Get(Guid id)
        {
            var sql = $@"
select * from {this.TableName} where _Id = @Id;
select * from BackupSources where ParentId = @Id;
select s1.* from BackupSourceConfigItems s1 inner join BackupSources s2 on s1.ParentId = s2._Id and s2.ParentId = @Id;
select * from BackupDestinations where ParentId = @Id;";

            var destConfigSql = $@"
select d1.* from BackupDestinationConfigItems d1 inner join BackupDestinations d2 on d1.ParentId = d2._Id and d2._Id = @Id;";

            using (var db = this.GetDb(false))
            {
                using (var multi = db.Connection.QueryMultiple(sql, new { Id = id.ToString() }))
                {
                    var syncConfig = multi.Read<SyncConfigurationPM>().Single();
                    syncConfig.Source = multi.Read<BackupSourcePM>().Single();
                    syncConfig.Source.Config = new Collection<SourceConfigItemPM>(multi.Read<SourceConfigItemPM>().ToList());
                    syncConfig.Destinations = new Collection<BackupDestinationPM>(multi.Read<BackupDestinationPM>().ToList());

                    foreach (var dest in syncConfig.Destinations)
                    {
                        dest.Config = new Collection<DestinationConfigItemPM>(db.Connection.Query<DestinationConfigItemPM>(destConfigSql, new { Id = dest.Id.ToString() }).ToList());
                    }
                    
                    return AutoMapper.Mapper.Map<SyncConfiguration>(syncConfig);
                }
            }
        }

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
                    result &= backupDestinationRepo.Save(destination);
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
