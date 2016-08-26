//-----------------------------------------------------------------------
// <copyright file="BackupSourceTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain.UnitTests
{
    using System.Reflection;
    using Xunit;
    
    /// <summary>
    /// Tests for the BackupSource class
    /// </summary>
    public class BackupSourceTests
    {
        /// <summary>
        /// Test the name property is assigned correctly
        /// </summary>
        [Fact]
        public void ConstructBackupSource_NameAppliedCorrectly()
        {
            var backupSource = new BackupSource("Synology NAS");
            Assert.Equal("Synology NAS", backupSource.Name);
        }

        [Fact]
        public void TestAssignmentOfTypeToEntity()
        {
            var backupSourceType = typeof(BackupSource);
            Assert.True(typeof(Entity).IsAssignableFrom(backupSourceType));
        }

    }
}
