using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using GSmartHR.Services.Users;
using GSmartHR.Web.Infrastructure;
namespace GSmartHR.Web.Areas.Admin.Models.Users
{
    public class EmployeeListModel
    {
        private readonly IEmployeeService __employeeService;
      
        public IEnumerable<EmployeeDetailsContainer> EmployeeList { get; set; }

        public EmployeeListModel()
        {
            __employeeService = DependencyResolver.GetService<IEmployeeService>();
        }
        public void LoadData(string employeeidno, string email, int pageno = 0, int pagesize = 0)
        {
            EmployeeList = __employeeService.GetAllEmployeesBySearch(employeeidno, email, pageno, pagesize);
           
        }
    }
}