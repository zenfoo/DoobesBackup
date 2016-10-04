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
    public abstract class Repository<DM,PM> : PersistenceRepository<PM>, IRepository<DM> 
        where DM : Entity
        where PM : PersistenceModel
    {
        //protected readonly string TableName;
        protected DbConnectionWrapper Db = null;

        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        /// <param name="tableName"></param>
        public Repository(string tableName) : base(tableName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Repository class and accepts an existing DbConnectionWrapper
        /// </summary>
        /// <param name="tableName">The name of the database table</param>
        /// <param name="wrapper">The db connection wrapper object</param>
        public Repository(
            string tableName, 
            DbConnectionWrapper wrapper) : base(tableName, wrapper)
        {
        }

        public new virtual DM Get(Guid id)
        {
            var pm = this.GetPM(id);
            return AutoMapper.Mapper.Map<DM>(pm);
        }
        
        public new virtual IEnumerable<DM> GetAll()
        {
            var pms = this.GetAllPM();
            return AutoMapper.Mapper.Map<IEnumerable<DM>>(pms);
        }
        
        public virtual bool Save(DM entity)
        {
            // Map to persistence model
            var pm = AutoMapper.Mapper.Map<PM>(entity);
            var success = this.SavePM(pm);
            if (success)
            {
                entity.SetId(pm.Id.Value);
            }
            return success;
        }

        public new virtual bool Delete(Guid id)
        {
            return base.Delete(id);
        }
        
        /// <summary>
        /// Expose direct access to the Save persistence model base class method
        /// </summary>
        /// <param name="pm">The persistence model object to save</param>
        /// <returns></returns>
        protected virtual bool SavePM(PM pm)
        {
            return base.Save(pm);
        }

        /// <summary>
        /// Exposes direct access to the Get persistence model base class method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual PM GetPM(Guid id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Exposes direct access to the GetAll persistence model base class method
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<PM> GetAllPM()
        {
            return base.GetAll();
        }
    }
}
