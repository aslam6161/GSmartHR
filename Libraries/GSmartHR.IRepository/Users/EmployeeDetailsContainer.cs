using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.IRepository.Users
{
    public class EmployeeDetailsContainer
    {
        public int TotalRows { get; set; }
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
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

        public Guid? UpdatedBy { get; set; }

        public Guid? CreatedBy { get; set; }

    }
}
