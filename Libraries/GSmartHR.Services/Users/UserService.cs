using System;
using System.Collections.Generic;
using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using GSmartHR.Core;
using System.Linq;

namespace GSmartHR.Services.Users
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        #endregion

        #region Ctor

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        #endregion

        public void InsertUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (user.Id == Guid.Empty)
            {
                user.Id = Guid.NewGuid();
            }


            _userRepository.Insert(user);
        }
        public void UpdateUser(User user)
        {


            if (user == null)
                throw new ArgumentNullException("user");


            _userRepository.Update(user);
        }
        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _userRepository.Delete(user);
        }

        public User GetUserById(Guid userId)
        {
            if (userId == Guid.Empty)
                return null;

            return _userRepository.GetById(userId);
        }

        public IEnumerable<User> GetUsersByUserType(int userType)
        {

            return _userRepository.GetUsersByUserType(userType);
        }


        public User GetUserByEmail(string usernameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var result = _userRepository.GetUserByEmail(usernameOrEmail);
            return result;
        }


        public bool UserAlreadyExist(string email, Guid Id)
        {
            var userAlreadyExist = _userRepository.UserAlreadyExist(email, Id);
            return userAlreadyExist;
        }

        public User GetUserByUserId(string usernameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var result = _userRepository.GetUserByUserId(usernameOrEmail);
            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<UserDetailsContainer> GetUsersByUsernameAndRoleName(string username, string rolename, int pageno, int pagesize)
        {
            return _userRepository.GetUsersByUsernameAndRoleName(username, rolename,pageno,pagesize);
        }
    }
}
