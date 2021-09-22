using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GSmartHR.Core.Domain.Users
{
    public class User : BaseActivity
    {
        public Guid RoleId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public UserRole UserRole { get; set; }

        public Employee Employee { get; set; }
    }
}
