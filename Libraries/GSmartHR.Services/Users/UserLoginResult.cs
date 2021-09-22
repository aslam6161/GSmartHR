using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Services.Users
{
    public class UserLoginResult
    {
        /// Ctor
        /// </summary>
        public UserLoginResult()
        {
            LoginStatus = new LoginStatus();
        }

        public LoginStatus LoginStatus { get; set; }
        public User User { get; set; }

    }
}
