using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using GSmartHR.Web.Model;
using GSmartHR.Services.Authentication;
using GSmartHR.Services.Users;
using GSmartHR.Core.Domain.Users;
using System.Reflection;
using GSmartHR.Web.Infrastructure;

namespace GSmartHR.Web.Controllers
{
    public class UserController : Controller
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserService _userService;
        #endregion

        #region Ctor

        public UserController(IAuthenticationService authenticationService,
            IUserRegistrationService userRegistrationService,
            IUserService userService)
        {

            this._userRegistrationService = userRegistrationService;
            this._authenticationService = authenticationService;
            this._userService = userService;
     

        }

        #endregion

        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {


            if (ModelState.IsValid)
            {
                var loginResult = _userRegistrationService.ValidateUser(model.Email, model.Password);

                switch (loginResult.LoginStatus)
                {
                    case LoginStatus.Successful:
                        {
                            returnUrl = HttpUtility.UrlDecode(returnUrl);

                            var user = loginResult.User;

                            //sign in new user
                            _authenticationService.SignIn(user, model.RememberMe);

                            RedirectToActionResult redirectToRouteResult = null;
                            var userRoleService = DependencyResolver.GetService<IUserRoleService>();

                            var userType = userRoleService.GetUserRoleById(user.RoleId).RoleName;

                            if (userType== "Administrator")
                            {
                                redirectToRouteResult = RedirectToAction("index", "Home", new { area = "Admin" });
                            }
                            else if (userType=="Staff")
                            {
                                redirectToRouteResult = RedirectToAction("index", "Home", new { area = "Staff" });
                            }

                            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                return redirectToRouteResult;

                            return Redirect(returnUrl);
                        }
                    case LoginStatus.UserNotExist:
                        ModelState.AddModelError("", "Sorry this userid does not exist");
                        break;
                    case LoginStatus.NotActive:
                        ModelState.AddModelError("", "This userid is not active");
                        break;
                    case LoginStatus.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Password incorrect");
                        break;
                }
            }

            return View(model);
        }

        public ActionResult SignOut()
        {

            _authenticationService.SignOut();

            return RedirectToAction("Login");
        }


    }
}