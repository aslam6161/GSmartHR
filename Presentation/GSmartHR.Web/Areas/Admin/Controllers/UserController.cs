using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSmartHR.Services.Users;
using GSmartHR.Web.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GSmartHR.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        #region user list / save / delete

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult GetUsernameByFiter(string username, string rolename,int jtStartIndex = 0, int jtPageSize = 0)
        {
            UserListModel userListModel = new UserListModel();
            userListModel.LoadData(username, rolename, ((jtStartIndex)/ jtPageSize)+1, jtPageSize);
            return Json(new { Result = "OK", Records = userListModel.UserList, TotalRecordCount = userListModel.UserList.Count()>0?userListModel.UserList.First().TotalRows:0 });
        }

        public ActionResult SaveUser(Guid? id)
        {
            UserModel userModel = new UserModel();

            if (IsEditMode(id))
            {
                userModel = userModel.GetUser(id.Value);
            }
            userModel.PrepareModel();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult SaveUser(UserModel userModel)
        {
            if (!IsEditMode(userModel.Id))
            {
                ModelState.Remove("Password");
            }
 

            if (ModelState.IsValid)
            {
                UserRegistrationResult userRegistrationResult = userModel.SaveUser();

                if (userRegistrationResult.Success)
                {
                    return RedirectToAction("UserList");
                }

                foreach (var item in userRegistrationResult.Errors)
                {
                    ModelState.AddModelError("", item);
                }

            }

            userModel.PrepareModel();

            return View(userModel);
        }

        public ActionResult Delete(Guid id)
        {
            UserModel userModel = new UserModel();
            userModel.DeleteUser(id);
            return RedirectToAction("UserList");
        }
        #endregion user list / save / delete
    }
}
