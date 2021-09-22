using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using GSmartHR.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using GSmartHR.Services.Users;
using GSmartHR.Core.Domain.Users;
using System.ComponentModel.DataAnnotations;
using GSmartHR.Core;

namespace GSmartHR.Web.Areas.Admin.Models.Users
{

    public class UserModel : BaseGSmartHREntityModel
    {
        #region Proparties
    
        [Required(ErrorMessage = "Please specify Username")]
        [DisplayName("Username")]
        public string Username { get; set; }
        [DisplayName("Password")]
        public System.String Password { get; set; }
        [DisplayName("Active")]
        public System.Boolean IsActive { get; set; }


        #endregion Proparties


        #region Fields

        private IUserService _userService;
        private IWorkContext _workContext;
        private IUserRegistrationService _userRegistrationService;
        private IUserRoleService _userRoleService;

        #endregion Fields

        #region Ctor
        public UserModel()
        {
            _userService = DependencyResolver.GetService<IUserService>();
            _userRegistrationService = DependencyResolver.GetService<IUserRegistrationService>();
            _workContext = DependencyResolver.GetService<IWorkContext>();
            _userRoleService = DependencyResolver.GetService<IUserRoleService>();

        }
        #endregion Ctor

        #region PrepareModel
        public void PrepareModel()
        {
        }
        #endregion PrepareModel

        #region Save

        public UserRegistrationResult SaveUser()
        {
            UserRegistrationResult userRegistrationResult = null;

            User user = new User();

            if (IsEditMode(this.Id))
            {
                user = _userService.GetUserById(this.Id.Value);

                ConvertFromModel(user);

                if (!string.IsNullOrEmpty(this.Password))
                {
                    user.PasswordHash = this.Password;
                }

                user.UpdatedBy = _workContext.CurrentUser.Id;

                user.UpdatedDate = DateTime.Now;

                userRegistrationResult = _userRegistrationService.UpdateRegisteredUser(user, !string.IsNullOrEmpty(this.Password));
            }
            else
            {
                ConvertFromModel(user);

                user.CreatedBy = _workContext.CurrentUser.Id;
                user.UpdatedBy = _workContext.CurrentUser.Id;
                user.PasswordHash = this.Password;
                user.CreatedDate = DateTime.Now;
                user.UpdatedDate = DateTime.Now;

                userRegistrationResult = _userRegistrationService.RegisterUser(user);
            }

            return userRegistrationResult;
        }

        #endregion Save

        #region Delete

        public void DeleteUser(Guid id)
        {
            var user = _userService.GetUserById(id);
            _userService.DeleteUser(user);
        }

        #endregion Delete

        public UserModel GetUser(Guid id)
        {
            var user = _userService.GetUserById(id);
            var userModel = ConvertToModel(user);
            return userModel;
        }


        #region model conversion

        private void ConvertFromModel(User user)
        {
            ////user
            ///
            var role = _userRoleService.GetUserRoleByName("Administrator");

            user.RoleId = role.Id;

            user.Username = this.Username;
            user.PasswordHash = this.Password;
            user.IsActive = this.IsActive;

        }

        private UserModel ConvertToModel(User user)
        {
            //user
            UserModel userModel = new UserModel();
            userModel.Id = user.Id;
            userModel.Username = user.Username;
            userModel.IsActive = user.IsActive;

            return userModel;
        }

        #endregion
    }
}