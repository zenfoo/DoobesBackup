//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Tests for the SyncConfiguration class
    /// </summary>
    public class SyncConfigurationTests
    {
        [Fact]
        public void AddBackupDestination_DuplicateDestination_Throws()
        {
            var sc = new SyncConfiguration("Test configuration");
            var destination = new BackupDestination("Some destination", "FileTarget");
            sc.AddBackupDestination(destination);
            var exception = Assert.Throws(typeof(DomainException), () => sc.AddBackupDestination(destination));
        }
    }
}
