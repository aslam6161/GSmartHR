using GSmartHR.Core;
using GSmartHR.Core.Domain;
using GSmartHR.Core.Domain.Users;
using GSmartHR.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Services.Users
{
    public class UserRegistrationService : IUserRegistrationService
    {

        #region Fields

        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;

        #endregion

        #region Ctor

        public UserRegistrationService(IUserService userService,
            IEncryptionService encryptionService)
        {
            this._userService = userService;
            this._encryptionService = encryptionService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual UserLoginResult ValidateUser(string usernameOrEmail, string password)
        {

            UserLoginResult userLoginResult = new UserLoginResult();

            var user = _userService.GetUserByUserId(usernameOrEmail);

            if (user == null)
            {
                userLoginResult.LoginStatus = LoginStatus.UserNotExist;

                return userLoginResult;
            }

            if (!user.IsActive)
            {
                userLoginResult.LoginStatus = LoginStatus.NotActive;

                return userLoginResult;
            }

            var pwd = _encryptionService.GetHashPassword(password,user.PasswordSalt);
            bool isValid = pwd == user.PasswordHash;

            if (!isValid)
            {
                userLoginResult.LoginStatus = LoginStatus.WrongPassword;

                return userLoginResult;
            }


            _userService.UpdateUser(user);

            userLoginResult.LoginStatus = LoginStatus.Successful;
            userLoginResult.User = user;
            return userLoginResult;
        }


        /// <returns>Result</returns>
        public virtual UserRegistrationResult RegisterUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var result = new UserRegistrationResult();

            if (String.IsNullOrEmpty(user.Username))
            {
                result.AddError("Account Register Errors user name Is Not Provided");
                return result;
            }

            if (String.IsNullOrWhiteSpace(user.PasswordHash))
            {
                result.AddError("Account Register Errors PasswordIsNotProvided");
                return result;
            }


            //validate unique user

            if (_userService.UserAlreadyExist(user.Username, user.Id))
            {
                result.AddError("Account Register Errors UserId Already Exists");
                return result;
            }

            var passwordSalt = _encryptionService.GenerateSalt();
            var password = _encryptionService.GetHashPassword(user.PasswordHash,passwordSalt);

            user.PasswordHash = password;
            user.PasswordSalt = passwordSalt;

            _userService.InsertUser(user);

            return result;
        }


        /// <returns>Result</returns>
        public virtual UserRegistrationResult UpdateRegisteredUser(User user,bool passwordChanged= false)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var result = new UserRegistrationResult();

            if (String.IsNullOrEmpty(user.Username))
            {
                result.AddError("Account Register Errors Email Is Not Provided");
                return result;
            }




            //validate unique user

            if (_userService.UserAlreadyExist(user.Username, user.Id))
            {
                result.AddError("Account Register Errors UserId Already Exists");
                return result;
            }


            if (passwordChanged)
            {
                var passwordSalt = _encryptionService.GenerateSalt();
                var password = _encryptionService.GetHashPassword(user.PasswordHash, passwordSalt);

                user.PasswordHash = password;
                user.PasswordSalt = passwordSalt;
            }

            _userService.UpdateUser(user);

            return result;
        }


        public virtual ChangePasswordResult ChangePassword(Guid userId, string password)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException("userId");

            var result = new ChangePasswordResult();

            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("userId");
            }

            var user = _userService.GetUserById(userId);

            //user.Password = _encryptionService.EncryptText(password);

            _userService.UpdateUser(user);


            return result;
        }


        public virtual bool MathchPassword(User user, string password)
        {
            if (user==null)
                throw new ArgumentNullException("user");

            if (String.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            //var encrypted = _encryptionService.EncryptText(password);


            return false;

            //return user.Password== encrypted;
        }


        #endregion


    }
}
