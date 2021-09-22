
using GSmartHR.Core.Domain;
using System;
namespace GSmartHR.Core.Domain.Users
{
    public class Attendance:BaseActivity
    {
        public Guid EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public Employee Employee { get; set; }
    }
}
