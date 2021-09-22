using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Linq;
 using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;

namespace GSmartHR.Repository.Users
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IConfiguration config) : base(config)
        {
        }

        public UserRole GetUserRoleByName(string name)
        {
            var userRole = this.GetAll().FirstOrDefault(x => x.RoleName == name);

            return userRole;
        }
    }
}