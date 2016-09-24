//-----------------------------------------------------------------------
// <copyright file="DbHelperTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.UnitTests
{
    using Framework;
    using Dapper;
    using Xunit;
    using System;
    using PersistenceModels;
    using System.Linq;

    /// <summary>
    /// Tests for the DbHelperTests class
    /// </summary>
    public class DbHelperTests
    {
        [Fact]
        public void GetColumnsForType_InvalidType_Throws()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                DbHelper.GetColumnsForType(typeof(string));
            });
        }
        
        [Fact]
        public void GetColumnsForType_ValidType_ReturnsSuccessfully()
        {
            var columns = DbHelper.GetColumnsForType(typeof(SyncConfigurationPM));
            Assert.True(columns.Count() > 0);
        }
    }
}
