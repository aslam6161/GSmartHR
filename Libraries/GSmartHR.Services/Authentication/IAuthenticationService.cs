using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.Services.Authentication
{
   public interface IAuthenticationService
    {
        void SignIn(User user, bool isPersistent);
        void SignOut();
    }
}
