using GSmartHR.IRepository.Users;
using GSmartHR.Services.Users;
using GSmartHR.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Areas.Admin.Models.Users
{
    public class AttendanceListModel
    {
        private readonly IAttendanceService _attendanceService;

        public IEnumerable<AttendanceDetailsContainer> AttendanceList { get; set; }

        public AttendanceListModel()
        {
            _attendanceService = DependencyResolver.GetService<IAttendanceService>();
        }
        public void LoadData(string employeeidno, int pageno = 0, int pagesize = 0)
        {
            AttendanceList = _attendanceService.GetAllAttendancesBySearch(employeeidno, pageno, pagesize);

        }
    }
}

