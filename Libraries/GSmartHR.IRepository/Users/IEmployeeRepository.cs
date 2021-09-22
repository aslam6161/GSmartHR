using System;
using System.Collections.Generic;
using System.Text;
 using GSmartHR.Core.Domain.Users;

namespace GSmartHR.IRepository.Users
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<EmployeeDetailsContainer> GetAllEmployeesBySearch(string employeeidno, string email, int pageno = 0, int pagesize = 0);
    }
}
