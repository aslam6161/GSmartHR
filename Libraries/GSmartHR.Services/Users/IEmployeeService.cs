using System;
using System.Collections.Generic;
using System.Text;
 using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;

namespace GSmartHR.Services.Users
{
    public interface IEmployeeService
    {
        void InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployeeById(Guid employeeId);
        IEnumerable<Employee> GetAllEmployee();
        IEnumerable<EmployeeDetailsContainer> GetAllEmployeesBySearch(string employeeidno, string email, int pageno = 0, int pagesize = 0);
    }
}
