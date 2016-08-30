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
    public abstract class Repository<DM,PM> : IRepository<DM> 
        where DM : class
        where PM : class
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
        public abstract bool Delete(Guid id);

        /// <inheritdoc />
        public abstract DM Get(Guid id);

        /// <inheritdoc />
        public abstract IEnumerable<DM> GetAll();

        /// <inheritdoc />
        public abstract bool Save(DM entity);
    }
}
