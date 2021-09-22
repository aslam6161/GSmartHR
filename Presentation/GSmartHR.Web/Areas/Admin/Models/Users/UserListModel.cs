
using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using GSmartHR.Services.Users;
using GSmartHR.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSmartHR.Web.Areas.Admin.Models.Users
{
    public class UserListModel
    {
        private readonly IUserService _userService;
        public IEnumerable<UserDetailsContainer> UserList { get; set; }
        public UserListModel()
        {
            _userService = DependencyResolver.GetService<IUserService>();
        }
        public void LoadData(string username,string rolename,int pageno,int pagesize)
        {
             UserList = _userService.GetUsersByUsernameAndRoleName(username, rolename, pageno, pagesize);
        }
    }
}