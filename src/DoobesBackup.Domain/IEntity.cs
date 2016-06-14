//-----------------------------------------------------------------------
// <copyright file="IAggregateRoot.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain
{
    using System;

    /// <summary>
    /// Generic interface for a domain entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique id for the entity
        /// </summary>
        Guid? Id { get; }
    }
}
