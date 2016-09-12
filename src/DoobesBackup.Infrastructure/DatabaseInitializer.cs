//-----------------------------------------------------------------------
// <copyright file="DatabaseInitializer.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
{
    using PersistenceModels;

    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            if (!DbHelper.TableExists("SyncConfigurations"))
            {
                DbHelper.CreateTable("SyncConfigurations", typeof(SyncConfigurationPM));
            }

            if (!DbHelper.TableExists("BackupSources"))
            {
                DbHelper.CreateTable("BackupSources", typeof(BackupSourcePM));
            }

            if (!DbHelper.TableExists("BackupDestinations"))
            {
                DbHelper.CreateTable("BackupDestinations", typeof(BackupDestinationPM));
            }

            if (!DbHelper.TableExists("BackupSourceConfigItems"))
            {
                DbHelper.CreateTable("BackupSourceConfigItems", typeof(SourceConfigItemPM));
            }

            if (!DbHelper.TableExists("BackupDestinationConfigItems"))
            {
                DbHelper.CreateTable("BackupDestinationConfigItems", typeof(DestinationConfigItemPM));
            }
        }
    }
}
