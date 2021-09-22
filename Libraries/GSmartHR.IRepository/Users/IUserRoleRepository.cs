using System;
using System.Collections.Generic;
using System.Text;
using GSmartHR.Core.Domain.Users;

namespace GSmartHR.IRepository.Users
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        UserRole GetUserRoleByName(string name);
    }
}
