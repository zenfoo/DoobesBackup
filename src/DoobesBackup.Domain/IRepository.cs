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
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T">The entity type to query against</typeparam>
    public interface IRepository<T> where T : class, IAggregateRoot
    {
        /// <summary>
        /// Retrieve an entity by it's id
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>The entity</returns>
        T Get(Guid id);

        /// <summary>
        /// Retrieve all the entities in the repository
        /// </summary>
        /// <returns>The collection of entities</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Save or update the entity specified
        /// </summary>
        /// <param name="entity">The entity to save or update</param>
        /// <returns>Boolean value indicating whether or not the operation succeeded</returns>
        bool Create(T entity);

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>Boolean value indicating whether or not the operation succeeded</returns>
        bool Update(T entity);

        /// <summary>
        /// Delete the specified entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);
    }
}
