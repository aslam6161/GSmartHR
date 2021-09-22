using GSmartHR.Services.Users;
using GSmartHR.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GSmartHR.Web.Areas.Admin.Models.Users
{

    public class UserChanePasswordModel : BaseGSmartHREntityModel
    {
        [Required(ErrorMessage ="Please specify password")]
        public string Password { get; set; }

        #region Fields

  
        private IUserRegistrationService _userRegistrationService;

        #endregion Fields

        #region Ctor

        public UserChanePasswordModel()
        {
            _userRegistrationService = DependencyResolver.GetService<IUserRegistrationService>();
        }
        #endregion Ctor

        internal ChangePasswordResult ChanePassword()
        {
           var result= _userRegistrationService.ChangePassword(this.Id.Value, this.Password);

           return result;
        }
    }
}