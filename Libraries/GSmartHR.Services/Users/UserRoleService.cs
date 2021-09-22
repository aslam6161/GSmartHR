using System;
using System.Collections.Generic;
 using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using GSmartHR.Core;

namespace GSmartHR.Services.Users
{
    public class UserRoleService : IUserRoleService
    {
        #region Fields

        private readonly IUserRoleRepository _userRoleRepository;


        #endregion

        #region Ctor

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            this._userRoleRepository = userRoleRepository;
        }

        #endregion

        public void InsertUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException("userRole");

            if (userRole.Id == Guid.Empty)
            {
                userRole.Id = Guid.NewGuid();
            }

            _userRoleRepository.Insert(userRole);
        }
        public void UpdateUserRole(UserRole userRole)
        {

            if (userRole == null)
                throw new ArgumentNullException("userRole");


            _userRoleRepository.Update(userRole);
        }
        public void DeleteUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException("userRole");

            _userRoleRepository.Delete(userRole);
        }

        public UserRole GetUserRoleById(Guid userRoleId)
        {
            if (userRoleId == Guid.Empty)
                return null;

            return _userRoleRepository.GetById(userRoleId);
        }

        public IEnumerable<UserRole> GetAllUserRole()
        {

            return _userRoleRepository.GetAll();
        }

        public UserRole GetUserRoleByName(string name)
        {

            return _userRoleRepository.GetUserRoleByName(name);
        }
    }
}
