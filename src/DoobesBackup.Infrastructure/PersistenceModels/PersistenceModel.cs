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
        /// Backing store for the id property so the field can be deserialised properly
        /// </summary>
        protected string _Id { get; set; }
    
        /// <summary>
        /// The unique id for the entity
        /// </summary>
        public virtual Guid? Id {
            get
            {
                Guid guid;
                if (Guid.TryParse(this._Id, out guid))
                {
                    return guid;
                }

                return null;
            }
            set
            {
                this._Id = value.HasValue ? value.Value.ToString() : null;
            }
        }
    }
}
