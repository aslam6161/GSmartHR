using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.Services.Users
{
    public interface IUserService
    {
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User GetUserById(Guid userId);
        IEnumerable<User> GetUsersByUserType(int userType);
        User GetUserByEmail(string usernameOrEmail);
        bool UserAlreadyExist(string email, Guid Id);
        User GetUserByUserId(string usernameOrEmail);
        IEnumerable<User> GetAllUsers();
        IEnumerable<UserDetailsContainer> GetUsersByUsernameAndRoleName(string username, string rolename, int pageno, int pagesize);
    }
}
