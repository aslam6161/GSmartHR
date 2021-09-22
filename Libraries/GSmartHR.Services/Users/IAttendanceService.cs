using System;
using System.Collections.Generic;
using System.Text;
 using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;

namespace GSmartHR.Services.Users
{
    public interface IAttendanceService
    {
        void InsertAttendance(Attendance attendance);
        void UpdateAttendance(Attendance attendance);
        void DeleteAttendance(Attendance attendance);
        Attendance GetAttendanceById(Guid attendanceId);
        IEnumerable<Attendance> GetAllAttendance();
        IEnumerable<AttendanceDetailsContainer> GetAllAttendancesBySearch(string attendanceidno, int pageno, int pagesize);
        Attendance GetAttendanceByEmployeeIdAndDate(Guid? employeeId, DateTime date);
    }
}
