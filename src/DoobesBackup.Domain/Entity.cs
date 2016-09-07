//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;

    /// <summary>
    /// Base class for domain entities
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// The unique id for the domain entity - if null, the entity is transient and has not been saved to persistent storage
        /// </summary>
        public virtual Guid? Id { get; protected set; }

        /// <summary>
        /// Set the id for the domain entity
        /// </summary>
        /// <param name="id">The unique id for this entity</param>
        public virtual void SetId(Guid id)
        {
            if (this.Id.HasValue)
            {
                throw new DomainException("The entity already has an id");
            }

            this.Id = id;
        }
    }
}
