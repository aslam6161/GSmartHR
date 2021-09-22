
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GSmartHR.Core.Domain.Users;
using GSmartHR.Web.Areas.Admin.Models.Users;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;

namespace GSmartHR.Web.Areas.Admin.Controllers
{
    public class EmployeeController : BaseAdminController
    {
        private readonly IHostingEnvironment _webHostEnvironment;
        public EmployeeController(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult EmployeeList()
        {
            return View();
        }
        public IActionResult GetEmployeesByFiter(string employeeidno, string email, int jtStartIndex = 0, int jtPageSize = 0)
        {
            EmployeeListModel employeeListModel = new EmployeeListModel();
            employeeListModel.LoadData(employeeidno, email, ((jtStartIndex) / jtPageSize) + 1, jtPageSize);
            return Json(new { Result = "OK", Records = employeeListModel.EmployeeList, TotalRecordCount = employeeListModel.EmployeeList.Count() > 0 ? employeeListModel.EmployeeList.First().TotalRows : 0 });
        }

        public ActionResult SaveEmployee(Guid? id)
        {
            EmployeeModel employeeModel = new EmployeeModel();

            if (IsEditMode(id))
            {
                employeeModel = employeeModel.GetEmployee(id.Value);
            }

            employeeModel.PrepareModel();

            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult SaveEmployee(EmployeeModel employeeModel)
        {
            if (!IsEditMode(employeeModel.Id) && string.IsNullOrEmpty(employeeModel.Password))
            {
                ModelState.AddModelError("", "Please specify password!");
            }
     

            if (ModelState.IsValid)
            {
                var success = employeeModel.SaveEmployee();

                if (success)
                {
                    return RedirectToAction("EmployeeList");
                }
                else
                {
                    ModelState.AddModelError("", "Employee Id  already exist!");
                }

            }

            
            employeeModel.PrepareModel();


            return View(employeeModel);
        }

        public ActionResult Delete(Guid id)
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                employeeModel.DeleteEmployee(id);

            }
            catch (Exception ex)
            {
                TempData["Message"] = "You can not delete this employee beacuse this employee is used in another entitiy";

            }
            return RedirectToAction("EmployeeList");
        }

        public IActionResult Report()
        {
            EmployeeListModel employeeListModel = new EmployeeListModel();
            employeeListModel.LoadData(string.Empty, string.Empty,1,100);

            string mimtype = "";
            int extention = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Employee.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("rd1", "Welcome to Hr");

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", employeeListModel.EmployeeList);

            var result = localReport.Execute(RenderType.Pdf, extention, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }
    }
}