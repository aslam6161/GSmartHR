using GSmartHR.Core.Domain;
using System;

namespace GSmartHR.Core.Domain.Users
{
    public class Employee:BaseActivity
    {
        public string EmployeeIdNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string NationalId { get; set; }
        public string OfficeName { get; set; }
        public string Department { get; set; }
        public string Designaton { get; set; }
        public string EmployeeImagePath { get; set; }

    }
}
