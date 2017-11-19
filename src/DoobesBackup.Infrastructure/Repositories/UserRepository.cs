//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationRepository.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.Repositories
{
    using System;
    using Dapper;
    using DoobesBackup.Domain;
    using PersistenceModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Implementation of the SyncConfiguration repository
    /// </summary>
    public class UserRepository : Repository<User, UserPM>, IUserRepository
    {
        public UserRepository() : base("Users") { }

        public User GetByUserName(string userName)
        {
            var query = $@"
select 
    * 
from 
    {this.TableName}
where 
    UserName = @UserName
";
            using (var db = this.GetDb(false))
            {
                var pm = db.Connection.QuerySingle<UserPM>(query, new { UserName = userName });
                return AutoMapper.Mapper.Map<User>(pm);
            }
        }
    }
}
