﻿//-----------------------------------------------------------------------
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

        public override IEnumerable<SyncConfiguration> GetAll()
        {
            var sql = $@"
select * from {this.TableName} order by _Id;
select * from BackupSources t1 inner join SyncConfigurations t2 on t1.ParentId = t2._Id order by ParentId;
select * from BackupDestinations t1 inner join SyncConfigurations t2 on t1.ParentId = t2._Id order by ParentId;
select * from BackupSourceConfigItems t1 inner join BackupSources t2 on t1.ParentId = t2._Id order by ParentId;
select * from BackupDestinationConfigItems t1 inner join BackupDestinations t2 on t1.ParentId = t2._Id order by ParentId;";

            using (var db = this.GetDb(false))
            {
                using (var multi = db.Connection.QueryMultiple(sql))
                {
                    // Extract the data from each dataset
                    var syncConfigs = multi.Read<SyncConfigurationPM>();
                    var backupSources = multi.Read<BackupSourcePM, SyncConfigurationPM, BackupSourcePM>(
                        (first, second) =>
                        {
                            first.Parent = second;
                            return first;
                        }, "_Id");
                    var backupDestinations = multi.Read<BackupDestinationPM, SyncConfigurationPM, BackupDestinationPM>(
                        (first, second) =>
                        {
                            first.Parent = second;
                            return first;
                        }, "_Id");
                    var sourceConfigItems = multi.Read<SourceConfigItemPM, BackupSourcePM, SourceConfigItemPM>(
                        (first, second) =>
                        {
                            first.Parent = second;
                            return first;
                        }, "_Id");
                    var destinationConfigItems = multi.Read<DestinationConfigItemPM, BackupDestinationPM, DestinationConfigItemPM>(
                        (first, second) =>
                        {
                            first.Parent = second;
                            return first;
                        }, "_Id");


                    // From lowest to highest scan through the records and build up the sync config items with their children
                    foreach (var destConfigItem in destinationConfigItems)
                    {
                        backupDestinations.Single(bd => bd.Id == destConfigItem.Parent.Id).Config.Add(destConfigItem);
                    }

                    foreach (var sourceConfigItem in sourceConfigItems)
                    {
                        backupSources.Single(bs => bs.Id == sourceConfigItem.Parent.Id).Config.Add(sourceConfigItem);
                    }

                    foreach (var backupSource in backupSources)
                    {
                        syncConfigs.Single(sc => sc.Id == backupSource.Parent.Id).Source = backupSource;
                    }

                    foreach (var backupDestination in backupDestinations)
                    {
                        syncConfigs.Single(sc => sc.Id == backupDestination.Parent.Id).Destinations.Add(backupDestination);
                    }
                    
                    return AutoMapper.Mapper.Map<IEnumerable<SyncConfiguration>>(syncConfigs);
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
