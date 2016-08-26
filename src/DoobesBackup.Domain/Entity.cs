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
        public virtual Guid? Id { get; protected set; }
    }
}
