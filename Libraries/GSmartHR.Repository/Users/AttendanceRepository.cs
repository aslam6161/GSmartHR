using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Linq;
using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;

namespace GSmartHR.Repository.Users
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<AttendanceDetailsContainer> GetAllAttendancesBySearch(string employeeidno, int pageno, int pagesize)
        {
            IEnumerable<AttendanceDetailsContainer> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<AttendanceDetailsContainer>("GetAttendancesBySearch", new { employeeidno = employeeidno, pageno = pageno, pagesize = pagesize }, commandType: CommandType.StoredProcedure);

                cn.Close();
            }
            return items;
        }

        public Attendance GetAttendanceByEmployeeIdAndDate(Guid? employeeId, DateTime date)
        {
            //i should be replace with storedprocedure or sql query, i have done it for saving tim
            var result =this.GetAll().Where(x => x.EmployeeId == employeeId && x.StartDateTime.Date == date).FirstOrDefault();
            return result;
        }
    }
}