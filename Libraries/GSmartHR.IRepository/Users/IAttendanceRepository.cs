using System;
using System.Collections.Generic;
using System.Text;
 using GSmartHR.Core.Domain.Users;

namespace GSmartHR.IRepository.Users
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        IEnumerable<AttendanceDetailsContainer> GetAllAttendancesBySearch(string employeeidno,  int pageno, int pagesize);
        Attendance GetAttendanceByEmployeeIdAndDate(Guid? employeeId, DateTime date);
    }
}
