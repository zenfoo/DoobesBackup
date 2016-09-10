//-----------------------------------------------------------------------
// <copyright file="PersistenceRepository.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Dapper;
    using Repositories;
    using PersistenceModels;
    using System.Text;
    using Microsoft.Data.Sqlite;
    using System.Dynamic;
    using System.Reflection;

    /// <summary>
    /// Repository internal to the persistence layer, used to store child persistence models
    /// </summary>
    /// <typeparam name="T">The entity type the repository deals with</typeparam>
    public abstract class PersistenceRepository<PM> : IRepository<PM>
        where PM : PersistenceModel
    {
        protected readonly string TableName;
        private DbConnectionWrapper Db = null;

        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        /// <param name="tableName"></param>
        public PersistenceRepository(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("The table name specified is not valid", "tableName");
            }

            this.TableName = tableName;
        }

        /// <summary>
        /// Initializes a new instance of the Repository class and accepts an existing DbConnectionWrapper
        /// </summary>
        /// <param name="tableName">The name of the database table</param>
        /// <param name="wrapper">The db connection wrapper object</param>
        public PersistenceRepository(string tableName, DbConnectionWrapper wrapper) : this(tableName)
        {
            this.Db = wrapper;
        }

        public virtual PM Get(Guid id)
        {
            using (var db = this.GetDb())
            {
                var query = $@"
select 
    * 
from 
    {this.TableName}
where 
    Id = @Id
";
                return db.Connection.QueryFirst<PM>(query, new { Id = id });
            }
        }
        
        public virtual IEnumerable<PM> GetAll()
        {
            using (var db = this.GetDb())
            {
                var query = $@"
select 
    * 
from
    {this.TableName}
";
                return db.Connection.Query<PM>(query);
            }
        }
        
        public virtual bool Save(PM pm)
        {   
            // Insert or update depending on id assignment
            using (var db = this.GetDb())
            {
                string query = null;
                bool isUpdate = pm.Id.HasValue;
                var queryParams = new Dictionary<string, object>();

                // Perform the update or insert
                if (isUpdate)
                {
                    query = this.GetUpdateQuery(pm, out queryParams);
                }
                else
                {
                    // Generate the Id for a new record
                    pm.Id = Guid.NewGuid();
                    query = this.GetInsertQuery(pm, out queryParams);
                }

                // Execute the query
                return db.Connection.Execute(query, queryParams) > 0; // Success if > 0 rows modified
            }
        }

        public virtual bool Delete(Guid id)
        {
            using (var db = this.GetDb())
            {
                var query = $@"
delete from 
    {this.TableName}
where 
    Id = @Id
";
                var result = db.Connection.Execute(query, new { Id = id });
                return result == 1;
            }
        }
        
        protected DbConnectionWrapper GetDb(bool startTransaction = false)
        {
            if (this.Db == null || this.Db.IsDisposed)
            {
                var connection = DbHelper.GetDbConnection();
                var wrapper = new DbConnectionWrapper(connection);
                if (startTransaction)
                {
                    wrapper.StartTransaction();
                }

                this.Db = wrapper;
            }

            this.Db.AddNestLevel();
            return this.Db;
        }
        
        private string GetUpdateQuery(PM pm, out Dictionary<string, object> queryParams)
        {
            queryParams = new Dictionary<string, object>();
            var columns = DbHelper.GetColumnsForType<PM>();
            var sb = new StringBuilder();
            sb.Append($@"
update  
    {this.TableName} 
set");
            var ii = 0;
            foreach (var column in columns)
            {
                // Retrieve the appropriate value to store
                queryParams[column.Name] = GetPropertyValueFromPath(pm, column.PropertyPath);

                // Add the query line
                sb.Append(string.Format("{0}{1} = @{1}",
                    ii > 0 ? "," : string.Empty,
                    column.Name));
                ii++;
            }
            sb.Append(@"
where   
    Id = @Id");

            return sb.ToString();
        }

        private string GetInsertQuery(PM pm, out Dictionary<string, object> queryParams)
        {
            queryParams = new Dictionary<string, object>();
            var columns = DbHelper.GetColumnsForType<PM>();
            var sb = new StringBuilder();
            sb.Append($@"
insert into  
    {this.TableName} 
(");
            var ii = 0;
            foreach (var column in columns)
            {
                // Retrieve the appropriate value to store
                queryParams[column.Name] = GetPropertyValueFromPath(pm, column.PropertyPath);

                // Add the initial query part for this column
                sb.Append(string.Format("{0}{1}",
                    ii > 0 ? "," : string.Empty,
                    column.Name));
                ii++;
            }
            sb.Append(") values (");
            ii = 0;
            foreach (var column in columns)
            {
                // Add the second query part for this column
                sb.Append(string.Format("{0}@{1}",
                    ii > 0 ? "," : string.Empty,
                    column.Name));
                ii++;
            }
            sb.Append(")");
            return sb.ToString();
        }
        
        /// <summary>
        /// Thanks Jon Skeet:
        /// http://stackoverflow.com/a/366339/540151
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static object GetPropertyValueFromPath(object value, string path)
        {
            Type currentType = value.GetType();
            foreach (string propertyName in path.Split('.'))
            {
                PropertyInfo property = currentType.GetProperty(propertyName);
                if (property == null)
                {
                    return null;
                }
                value = property.GetValue(value);
                currentType = property.PropertyType;
            }

            return value;
        }
    }
}
