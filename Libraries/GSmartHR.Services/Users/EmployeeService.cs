using System;
using System.Collections.Generic;
using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using GSmartHR.Core;

namespace GSmartHR.Services.Users
{
    public class EmployeeService : IEmployeeService
    {
        #region Fields
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        #region Ctor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        #endregion

        public void InsertEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException("employee");

            if (employee.Id == Guid.Empty)
            {
                employee.Id = Guid.NewGuid();
            }

            _employeeRepository.Insert(employee);
        }
        public void UpdateEmployee(Employee employee)
        {

            if (employee == null)
                throw new ArgumentNullException("employee");

   

            _employeeRepository.Update(employee);
        }
        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException("employee");


            _employeeRepository.Delete(employee);
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
                return null;

            return _employeeRepository.GetById(employeeId);
        }

        public IEnumerable<Employee> GetAllEmployee()
        {

            return _employeeRepository.GetAll();
        }

        public IEnumerable<EmployeeDetailsContainer> GetAllEmployeesBySearch(string employeeidno, string email, int pageno = 0, int pagesize = 0)
        {
            return _employeeRepository.GetAllEmployeesBySearch(employeeidno, email, pageno, pagesize);
        }
    }
}
