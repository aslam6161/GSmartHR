using GSmartHR.Web.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Areas.Admin.Controllers
{
    public class AttendanceController : BaseAdminController
    {
        public IActionResult AttendanceList()
        {
            return View();
        }
        public IActionResult GetAttendancesByFiter(string employeeidno, int jtStartIndex = 0, int jtPageSize = 0)
        {
            AttendanceListModel attendanceListModel = new AttendanceListModel();
            attendanceListModel.LoadData(employeeidno, ((jtStartIndex) / jtPageSize) + 1, jtPageSize);
            return Json(new { Result = "OK", Records = attendanceListModel.AttendanceList, TotalRecordCount = attendanceListModel.AttendanceList.Count() > 0 ? attendanceListModel.AttendanceList.First().TotalRows : 0 });
        }
    }
}
