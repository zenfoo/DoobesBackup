//-----------------------------------------------------------------------
// <copyright file="DbConnectionWrapperTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.UnitTests
{
    using Framework;
    using Dapper;
    using Xunit;
    using System;

    /// <summary>
    /// Tests for the DbConnectionWrapperTests class
    /// </summary>
    public class DbConnectionWrapperTests
    {
        [Fact]
        public void SingleLevelWrapper_CorrectlyDisposed()
        {
            DbHelper.DeleteDb("test.db");
            var connection = DbHelper.GetDbConnection("test.db");
            DbConnectionWrapper wrapper;

            using (wrapper = new DbConnectionWrapper(connection))
            {
                wrapper.Connection.Execute("SELECT 'TEST'");
            }

            Assert.True(wrapper.IsDisposed);
            Assert.Null(wrapper.Connection);
        }

        [Fact]
        public void NestedWrapper_CorrectlyDisposed()
        {
            DbHelper.DeleteDb("test.db");
            var connection = DbHelper.GetDbConnection("test.db");
            DbConnectionWrapper wrapper;

            using (wrapper = new DbConnectionWrapper(connection))
            {
                wrapper.Connection.Execute("SELECT 'TEST'");

                using (wrapper.AddScope())
                {
                    wrapper.Connection.Execute("SELECT 'TEST2'");
                }
            }

            Assert.True(wrapper.IsDisposed);
            Assert.Null(wrapper.Connection);
        }

        [Fact]
        public void SingleLevelWrapper_UncomittedTransactionRolledBack()
        {
            DbHelper.DeleteDb("test.db");
            var connection = DbHelper.GetDbConnection("test.db");

            using (var wrapper = new DbConnectionWrapper(connection))
            {
                wrapper.Connection.Execute("CREATE TABLE TestTable (ID INTEGER PRIMARY KEY)");
                // Uncomitted
            }

            Assert.False(DbHelper.TableExists("TestTable"));
        }

        [Fact]
        public void SingleLevelWrapper_ComittedStatementsPersist()
        {
            DbHelper.DeleteDb("test.db");
            var connection = DbHelper.GetDbConnection("test.db");
            
            using (var wrapper = new DbConnectionWrapper(connection))
            {
                wrapper.Connection.Execute("CREATE TABLE TestTable (ID INTEGER PRIMARY KEY)");
                wrapper.Commit();
            }

            Assert.True(DbHelper.TableExists("TestTable", "test.db"));
        }

        [Fact]
        public void NestedWrapper_CommittedOuterTransaction_AllStatementsPersist()
        {
            DbHelper.DeleteDb("test.db");
            var connection = DbHelper.GetDbConnection("test.db");

            using (var wrapper = new DbConnectionWrapper(connection))
            {
                wrapper.Connection.Execute("CREATE TABLE OuterTable (ID INTEGER PRIMARY KEY)");
                
                using (wrapper.AddScope())
                {
                    wrapper.Connection.Execute("CREATE TABLE InnerTable (ID INTEGER PRIMARY KEY)");
                }

                wrapper.Commit();
            }

            Assert.True(DbHelper.TableExists("InnerTable", "test.db"));
            Assert.True(DbHelper.TableExists("OuterTable", "test.db"));
        }
    }
}
