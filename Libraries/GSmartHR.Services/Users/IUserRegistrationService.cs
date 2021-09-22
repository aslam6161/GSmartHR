using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Services.Users
{
    public interface IUserRegistrationService
    {
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        UserLoginResult ValidateUser(string usernameOrEmail, string password);

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        UserRegistrationResult RegisterUser(User user);

        UserRegistrationResult UpdateRegisteredUser(User user,bool passwordChanged=false);

        ChangePasswordResult ChangePassword(Guid userId, string password);

        bool MathchPassword(User user, string password);
    }
}
