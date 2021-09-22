
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GSmartHR.Core.Domain.Users;
using GSmartHR.Web.Areas.Staff.Models.Users;

namespace GSmartHR.Web.Areas.Staff.Controllers
{
    public class EmployeeController : BaseStaffController
    {

        public ActionResult SaveEmployee()
        {
    
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel = employeeModel.GetEmployee();
            employeeModel.PrepareModel();

            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult SaveEmployee(EmployeeModel employeeModel)
        {

            if (ModelState.IsValid)
            {
                var success = employeeModel.SaveEmployee();

                if (success)
                {
                    return RedirectToAction("SaveEmployee");
                }

            }

            employeeModel.PrepareModel();


            return View(employeeModel);
        }

    }
}