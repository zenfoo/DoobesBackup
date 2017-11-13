//-----------------------------------------------------------------------
// <copyright file="BackupDestinationTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain.UnitTests
{
    using Xunit;

    /// <summary>
    /// Tests for the BackupDestination class
    /// </summary>
    public class BackupDestinationTests
    {
        [Fact]
        public void ConstructBackupDestination_NameAppliedCorrectly()
        {
            var backupDestination = new BackupDestination("Synology NAS", "FileTarget");
            Assert.Equal("Synology NAS", backupDestination.Name);
        }
    }
}
