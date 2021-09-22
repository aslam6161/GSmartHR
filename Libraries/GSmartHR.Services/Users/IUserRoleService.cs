using System;
using System.Collections.Generic;
using System.Text;
 using GSmartHR.Core.Domain.Users;

namespace GSmartHR.Services.Users
{
    public interface IUserRoleService
    {
        void InsertUserRole(UserRole userRole);
        void UpdateUserRole(UserRole userRole);
        void DeleteUserRole(UserRole userRole);
        UserRole GetUserRoleById(Guid userRoleId);
        IEnumerable<UserRole> GetAllUserRole();
        UserRole GetUserRoleByName(string v);
    }
}
