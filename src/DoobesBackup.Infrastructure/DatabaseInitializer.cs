//-----------------------------------------------------------------------
// <copyright file="DatabaseInitializer.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
{
    using DoobesBackup.Domain;

    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            if (!DbHelper.TableExists("SyncConfiguration"))
            {
                DbHelper.CreateTable(typeof(SyncConfiguration));
            }
        }
    }
}
