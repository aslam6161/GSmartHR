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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration config) : base(config)
        {
        }


        public IEnumerable<EmployeeDetailsContainer> GetAllEmployeesBySearch(string employeeidno, string email, int pageno = 0, int pagesize = 0)
        {
            IEnumerable<EmployeeDetailsContainer> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                items = cn.Query<EmployeeDetailsContainer>("GetEmployeesBySearch", new { employeeidno = employeeidno, email = email, pageno = pageno, pagesize = pagesize }, commandType: CommandType.StoredProcedure);

                cn.Close();
            }
            return items;
        }
    }
}