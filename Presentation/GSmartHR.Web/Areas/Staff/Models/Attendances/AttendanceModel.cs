using GSmartHR.Core;
using GSmartHR.Core.Domain.Users;
using GSmartHR.Services.Users;
using GSmartHR.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Areas.Staff.Models.Attendances
{
    public class AttendanceModel
    {

        #region Propartie
       public int Status { get; set; }

        #endregion Proparties

        #region Fields
        private IEmployeeService _employeeService;
        private IWorkContext _workContext;
        private IUserRoleService _userRoleService;
        private IUserRegistrationService _userRegistrationService;
        private IUserService _userService;
        private IAttendanceService _attendanceService;

        #endregion Fields

        #region Ctor

        public AttendanceModel()
        {
            _employeeService = DependencyResolver.GetService<IEmployeeService>();
            _workContext = DependencyResolver.GetService<IWorkContext>();
            _userRoleService = DependencyResolver.GetService<IUserRoleService>();
            _userRegistrationService = DependencyResolver.GetService<IUserRegistrationService>();
            _userService = DependencyResolver.GetService<IUserService>();
            _attendanceService = DependencyResolver.GetService<IAttendanceService>();
        }

        #endregion Ctor

        #region PrepareModel
        public void PrepareModel()
        {
            var user = _userService.GetUserById(_workContext.CurrentUser.Id);

            var todaysAttendance = _attendanceService.GetAttendanceByEmployeeIdAndDate(user.EmployeeId, DateTime.Now.Date);
             
            if (todaysAttendance != null)
            {
                if(!todaysAttendance.EndDateTime.HasValue)
                {
                    this.Status = 2;
                }
                else
                {
                    this.Status = 3;
                }
              
            }
            else
            {
                this.Status = 1;
            }

        }
        #endregion PrepareModel

        #region Save
        public void SaveAttendance()
        {

            Attendance attendance = new Attendance();


            var user = _userService.GetUserById(_workContext.CurrentUser.Id);
          

            var todaysAttendance = _attendanceService.GetAttendanceByEmployeeIdAndDate(user.EmployeeId, DateTime.Now.Date);

            if (todaysAttendance != null)
            {
                attendance = todaysAttendance;
                attendance.EndDateTime = DateTime.Now;
                attendance.UpdatedBy = _workContext.CurrentUser.Id;
                attendance.UpdatedDate = DateTime.Now;
                _attendanceService.UpdateAttendance(attendance);

            }
            else
            {
                attendance.StartDateTime = DateTime.Now;
                attendance.EmployeeId = user.EmployeeId.Value;
                attendance.CreatedBy = _workContext.CurrentUser.Id;
                attendance.CreatedDate = DateTime.Now;
                attendance.UpdatedBy = _workContext.CurrentUser.Id;
                attendance.UpdatedDate = DateTime.Now;
                _attendanceService.InsertAttendance(attendance);

            }

        }
        #endregion Save


    }
}
