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
    using System.Linq.Expressions;
    using Dapper;

    /// <summary>
    /// Base implementation of the repository pattern using dapper
    /// </summary>
    /// <typeparam name="T">The base entity type the repository deals with</typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly string TableName;

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

        /// <inheritdoc />
        public virtual void Delete(T entity)
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                connection.Execute("delete from " + this.TableName + " where Id = @Id", new { Id = entity.Id });
            }
        }

        /// <inheritdoc />
        public virtual T Get(Guid id)
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                return connection.QueryFirst<T>("select * from " + this.TableName + " where Id = @Id", new { Id = id });
            }
        }

        /// <inheritdoc />
        public virtual IEnumerable<T> GetAll()
        {
            using (var connection = DbHelper.GetDbConnection())
            {
                return connection.Query<T>("select * from " + this.TableName);
            }
        }

        /// <inheritdoc />
        public abstract bool Create(T entity);
        
        /// <inheritdoc />
        public abstract bool Update(T entity);
        
    }
}
