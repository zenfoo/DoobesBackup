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
        /// <summary>
        /// Test the name property is assigned correctly
        /// </summary>
        [Fact]
        public void ConstructBackupSource_NameAppliedCorrectly()
        {
            var backupSource = new BackupDestination(null, "Synology NAS");
            Assert.Equal("Synology NAS", backupSource.Name);
        }
    }
}
