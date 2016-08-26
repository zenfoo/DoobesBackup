//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Generic repository pattern that can persist a domain model
    /// </summary>
    /// <typeparam name="DM">The domain model type</typeparam>
    public interface IRepository<DM> 
        where DM : class
    {
        /// <summary>
        /// Retrieve an entity by it's id
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>The entity</returns>
        DM Get(Guid id);

        /// <summary>
        /// Retrieve all the entities in the repository
        /// </summary>
        /// <returns>The collection of entities</returns>
        IEnumerable<DM> GetAll();

        /// <summary>
        /// Save or update the entity specified
        /// </summary>
        /// <param name="entity">The entity to save or update</param>
        /// <returns>Boolean value indicating whether or not the operation succeeded</returns>
        bool Save(DM entity);

        /// <summary>
        /// Delete the specified entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>Boolean value indicating whether or not the operation succeeded</returns>
        bool Delete(DM entity);
    }
}
