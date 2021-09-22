using Microsoft.AspNetCore.Http;
using GSmartHR.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;

namespace GSmartHR.Web.Infrastructure
{
    public partial class WebWorkContext : IWorkContext
    {

        #region Properties

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserModel CurrentUser { get; set; }
        public bool IsAuthenticated { get; set; }

        public WebWorkContext(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;

            if (authenticateResult.Succeeded)
            {
                var userClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == "User");
                if (userClaim != null)
                {
                    IsAuthenticated = true;
                    CurrentUser = JsonConvert.DeserializeObject<UserModel>(userClaim.Value);
                }
            }

        }

        public bool IsInRole(string userType)
        {
            if (CurrentUser == null)
            {
                return false;
            }

            if (CurrentUser.RoleName == userType)
            {
                return true;
            }

            return false;
        }



        #endregion

    }
}
