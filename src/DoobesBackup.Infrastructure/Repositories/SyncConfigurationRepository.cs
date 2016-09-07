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
        public SyncConfigurationRepository() : base("SyncConfigurations") { }
        
        public override bool Save(SyncConfiguration entity)
        {
            using (var db = this.GetDb(true))
            {
                var result = base.Save(entity);
                
                // Insert each destination record

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
