using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GSmartHR.Core;
using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using GSmartHR.IRepository.Users;

namespace GSmartHR.Services.Authentication
{
    public partial class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRoleRepository _userRoleRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="customerSettings">Customer settings</param>
        /// <param name="customerService">Customer service</param>
        /// <param name="httpContextAccessor">HTTP context accessor</param>
        public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor, IUserRoleRepository userRoleRepository)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._userRoleRepository = userRoleRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        public virtual async void SignIn(User user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for customer's username and email

            var userData = GetSerializedUserObject(user);

            var claims = new List<Claim>();
            claims.Add(new Claim("User", userData));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.Now
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

        
        }

        private string GetSerializedUserObject(User user)
        {
            UserModel userModel = new UserModel();
            userModel.Id = user.Id;
            userModel.UserName = user.Username;

            var userRole = _userRoleRepository.GetById(user.RoleId);

            userModel.RoleName = userRole.RoleName;

            string userData = JsonConvert.SerializeObject(userModel);

            return userData;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual async void SignOut()
        {
            //reset cached custome

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        #endregion
    }
}
