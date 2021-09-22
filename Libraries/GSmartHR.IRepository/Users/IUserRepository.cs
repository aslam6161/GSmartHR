using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.IRepository.Users
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsersByUserType(int userType);
        User GetUserByEmail(string usernameOrEmail);
        bool UserAlreadyExist(string email, Guid id);
        User GetUserByUserId(string usernameOrEmail);
        IEnumerable<UserDetailsContainer> GetUsersByUsernameAndRoleName(string username, string rolename, int pageno, int pagesize);
    }
}
