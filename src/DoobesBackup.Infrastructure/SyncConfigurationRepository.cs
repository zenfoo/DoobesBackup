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

    /// <summary>
    /// Implementation of the SyncConfiguration repository
    /// </summary>
    public class SyncConfigurationRepository : Repository<SyncConfiguration>, ISyncConfigurationRepository
    {
        public SyncConfigurationRepository() : base("SyncConfiguration") { }

        public override bool Create(SyncConfiguration entity)
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                var result = connection.Execute(
                    "insert into " + this.TableName + " (Id, IntervalSeconds, SourceId, DestinationId) values (@Id, @IntervalSeconds, @SourceId, @DestinationId)",
                    new {
                        Id = Guid.NewGuid(),
                        IntervalSeconds = entity.IntervalSeconds,
                        SourceId = entity.Source.Id,
                        DestinationId = entity.Destination.Id
                    });
                return result > 0;
            }
        }

        public override bool Update(SyncConfiguration entity)
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                var result = connection.Execute(
                    "update " + this.TableName + " set IntervalSeconds = @IntervalSeconds, SourceId = @SourceId, DestinationId = @DestinationId where Id = @Id",
                    new
                    {
                        Id = entity.Id,
                        IntervalSeconds = entity.IntervalSeconds,
                        SourceId = entity.Source.Id,
                        DestinationId = entity.Destination.Id
                    });

                return result > 0;
            }
        }
    }
}
