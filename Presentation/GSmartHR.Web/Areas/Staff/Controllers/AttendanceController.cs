using GSmartHR.Web.Areas.Staff.Models.Attendances;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Areas.Staff.Controllers
{
    public class AttendanceController : BaseStaffController
    {
        public IActionResult Attendance()
        {
            AttendanceModel attendance = new AttendanceModel();
            attendance.PrepareModel();
            return View(attendance);
        }

        public IActionResult SaveAttendance()
        {
            AttendanceModel attendance = new AttendanceModel();
            attendance.SaveAttendance();
            return RedirectToAction("Attendance");
        }

        public IActionResult AttendanceList()
        {
            return View();
        }
    }
}
