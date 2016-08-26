//-----------------------------------------------------------------------
// <copyright file="PersistenceModel.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.PersistenceModels
{
    using System;

    /// <summary>
    /// Generic interface for a domain entity
    /// </summary>
    public abstract class PersistenceModel
    {
        /// <summary>
        /// The unique id for the entity
        /// </summary>
        public virtual Guid? Id { get; set; }
    }
}
