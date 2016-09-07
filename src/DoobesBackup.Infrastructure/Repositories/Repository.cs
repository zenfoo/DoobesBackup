//-----------------------------------------------------------------------
// <copyright file="Repository.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using DoobesBackup.Domain;
    using Dapper;
    using Repositories;
    using PersistenceModels;
    using System.Text;
    using Microsoft.Data.Sqlite;

    /// <summary>
    /// Base implementation of the repository pattern using dapper
    /// </summary>
    /// <typeparam name="T">The base entity type the repository deals with</typeparam>
    public abstract class Repository<DM,PM> : IRepository<DM> 
        where DM : Entity
        where PM : PersistenceModel
    {
        protected readonly string TableName;
        private DbConnectionWrapper Db = null;

        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        /// <param name="tableName"></param>
        public Repository(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("The table name specified is not valid", "tableName");
            }

            this.TableName = tableName;
        }

        public virtual DM Get(Guid id)
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
                var result = db.Connection.QueryFirst<PM>(query, new { Id = id });
                return AutoMapper.Mapper.Map<DM>(result);
            }
        }
        
        public virtual IEnumerable<DM> GetAll()
        {
            using (var db = this.GetDb())
            {
                var query = $@"
select 
    * 
from
    {this.TableName}
";
                var results = db.Connection.Query<PM>(query);

                return AutoMapper.Mapper.Map<IEnumerable<DM>>(results);
            }
        }
        
        public virtual bool Save(DM entity)
        {
            // Map to persistence model
            var pm = AutoMapper.Mapper.Map<PM>(entity);

            var columns = DbHelper.GetColumnsForType<DM>();

            // Insert or update depending on id assignment
            using (var db = this.GetDb())
            {
                string query = null;
                bool isUpdate = pm.Id.HasValue;

                // Perform the update or insert
                if (isUpdate)
                {
                    query = this.GetUpdateQuery();
                }
                else
                {
                    // Generate the Id for a new record
                    pm.Id = Guid.NewGuid();
                    query = this.GetInsertQuery();
                }

                // Execute the query
                var result = db.Connection.Execute(query, pm);
                if (result > 0)
                {
                    if (!isUpdate)
                    {
                        // Hydrate entity with newly created id
                        entity.SetId(pm.Id.Value);
                    }
                    return true;
                }

                return false;
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

        private string GetUpdateQuery()
        {
            var columns = DbHelper.GetColumnsForType<DM>();
            var sb = new StringBuilder();
            sb.Append($@"
update  
    {this.TableName} 
set");
            var ii = 0;
            foreach (var column in columns)
            {
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

        private string GetInsertQuery()
        {
            var columns = DbHelper.GetColumnsForType<DM>();
            var sb = new StringBuilder();
            sb.Append($@"
insert into  
    {this.TableName} 
(");
            var ii = 0;
            foreach (var column in columns)
            {
                sb.Append(string.Format("{0}{1}",
                    ii > 0 ? "," : string.Empty,
                    column.Name));
                ii++;
            }
            sb.Append(") values (");
            ii = 0;
            foreach (var column in columns)
            {
                sb.Append(string.Format("{0}@{1}",
                    ii > 0 ? "," : string.Empty,
                    column.Name));
                ii++;
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
