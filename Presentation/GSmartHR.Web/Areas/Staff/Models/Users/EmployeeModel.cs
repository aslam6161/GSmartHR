using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GSmartHR.Core.Domain.Users;
using GSmartHR.Services.Users;
using GSmartHR.Web.Infrastructure;
using GSmartHR.Core;
using GSmartHR.Services.UploadHelper;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GSmartHR.Web.Areas.Staff.Models.Users
{
    public class EmployeeModel : BaseGSmartHREntityModel
    {
        #region Proparties

     
        public String EmployeeIdNo { get; set; }

        [Required]
        [DisplayName("First Name")]
        public String FirstName { get; set; }
        [DisplayName("Last Name")]
        public String LastName { get; set; }
        [Required]
        [DisplayName("Joining Date")]
        public DateTime JoiningDate { get; set; }
        [DisplayName("Email")]
        public String Email { get; set; }
        [DisplayName("Contact No")]
        public String ContactNo { get; set; }
        [DisplayName("National Id")]
        public String NationalId { get; set; }
        [DisplayName("Office Name")]
        public String OfficeName { get; set; }
        [DisplayName("Department")]
        public String Department { get; set; }
        [DisplayName("Designaton")]
        public String Designaton { get; set; }
        [DisplayName("Employee Image Path")]
        public String EmployeeImagePath { get; set; }
        public IFormFile ImageFile { get; set; }


        #endregion Proparties

        #region Fields
        private IEmployeeService _employeeService;
        private IWorkContext _workContext;
        private IUserRoleService _userRoleService;
        private IUserRegistrationService _userRegistrationService;
        private IUserService _userService;
        private IFileUploadService _fileUploadService;

        #endregion Fields

        #region Ctor

        public EmployeeModel()
        {
            _employeeService = DependencyResolver.GetService<IEmployeeService>();
            _workContext = DependencyResolver.GetService<IWorkContext>();
            _userRoleService = DependencyResolver.GetService<IUserRoleService>();
            _userRegistrationService = DependencyResolver.GetService<IUserRegistrationService>();
            _userService = DependencyResolver.GetService<IUserService>();

            _fileUploadService = DependencyResolver.GetService<IFileUploadService>();
            _fileUploadService.LoadDirectory("Uploads/Images");
        }

        #endregion Ctor

        #region PrepareModel
        public void PrepareModel()
        {
        }
        #endregion PrepareModel

        #region Save
        public bool SaveEmployee()
        {

            Employee employee = new Employee();


            var user = _userService.GetUserById(_workContext.CurrentUser.Id);

        
            user.UpdatedBy = _workContext.CurrentUser.Id;
            user.UpdatedDate = DateTime.Now;
            var res = _userRegistrationService.UpdateRegisteredUser(user);

            if (res.Success)
            {
                employee = _employeeService.GetEmployeeById(user.EmployeeId.GetValueOrDefault());
                ConvertFromModel(employee);
                employee.UpdatedDate = DateTime.Now;
                employee.UpdatedBy = _workContext.CurrentUser.Id;
                _employeeService.UpdateEmployee(employee);
            }

            return res.Success;


        }


        private void ConvertFromModel(Employee employee)
        {
            if (FileValidationExtension.ValidateImageFile(this.ImageFile) && FileValidationExtension.ValidateSize(ImageFile, 3))
            {

                var fileName = _fileUploadService.GetFileName(this.ImageFile);

                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);

                _fileUploadService.UploadFile(this.ImageFile, fileName);

                employee.EmployeeImagePath = fileName;
            }

            //employee.EmployeeIdNo = this.EmployeeIdNo;
            employee.FirstName = this.FirstName;
            employee.LastName = this.LastName;
            employee.JoiningDate = this.JoiningDate;
            employee.Email = this.Email;
            employee.ContactNo = this.ContactNo;
            employee.NationalId = this.NationalId;
            employee.OfficeName = this.OfficeName;
            employee.Department = this.Department;
            employee.Designaton = this.Designaton;
        }

        #endregion Save

        public EmployeeModel GetEmployee()
        {
            var user = _userService.GetUserById(_workContext.CurrentUser.Id);
            var employee = _employeeService.GetEmployeeById(user.EmployeeId.GetValueOrDefault());
            var employeeModel = ConvertToModel(employee);
            return employeeModel;
        }


        private EmployeeModel ConvertToModel(Employee employee)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeIdNo = employee.EmployeeIdNo;
            employeeModel.FirstName = employee.FirstName;
            employeeModel.LastName = employee.LastName;
            employeeModel.JoiningDate = employee.JoiningDate;
            employeeModel.Email = employee.Email;
            employeeModel.ContactNo = employee.ContactNo;
            employeeModel.NationalId = employee.NationalId;
            employeeModel.OfficeName = employee.OfficeName;
            employeeModel.Department = employee.Department;
            employeeModel.Designaton = employee.Designaton;
            employeeModel.EmployeeImagePath = employee.EmployeeImagePath;

            return employeeModel;
        }
    }
}
