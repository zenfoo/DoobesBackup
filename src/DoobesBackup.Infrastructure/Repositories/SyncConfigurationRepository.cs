//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationRepository.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
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
        public SyncConfigurationRepository() : base("SyncConfiguration") { }

        /// <inheritdoc />
        public override SyncConfiguration Get(Guid id)
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                var query = $@"
select 
    * 
from 
    {this.TableName}
where 
    Id = @Id
";
                var result = connection.QueryFirst<SyncConfigurationPM>(query, new { Id = id });
                return AutoMapper.Mapper.Map<SyncConfiguration>(result);
            }
        }

        /// <inheritdoc />
        public override IEnumerable<SyncConfiguration> GetAll()
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                var query = $@"
select 
    * 
from
    {this.TableName}
";
                var results = connection.Query<SyncConfigurationPM>(query);
                
                return AutoMapper.Mapper.Map<IEnumerable<SyncConfiguration>>(results);
            }
        }

        /// <inheritdoc />
        public override bool Save(SyncConfiguration entity)
        {
            // Map to persistence model
            var pm = AutoMapper.Mapper.Map<SyncConfigurationPM>(entity);

            // Insert or update depending on id assignment
            using (var connection = DbHelper.GetDbConnection())
            {
                if (pm.Id.HasValue)
                {

                    var query = $@"
update  
    {this.TableName} 
set     
    IntervalSeconds = @IntervalSeconds, 
    SourceId = @SourceId, 
    DestinationId = @DestinationId 
where   
    Id = @Id";
                    var result = connection.Execute(query, pm);
                    return result > 0;
                }
                else
                {
                    var query = $@"
insert into {this.TableName} (
    Id, 
    IntervalSeconds, 
    SourceId, 
    DestinationId
) values (
    @Id, 
    @IntervalSeconds, 
    @SourceId, 
    @DestinationId
)";
                    var result = connection.Execute(query, pm);
                    return result > 0;
                }
            }
        }

        /// <inheritdoc />
        public override bool Delete(SyncConfiguration entity)
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                var query = $@"
delete from 
    {this.TableName}
where 
    Id = @Id
";
                var result = connection.Execute(query, entity);
                return result == 1;
            }
        }
    }
}
