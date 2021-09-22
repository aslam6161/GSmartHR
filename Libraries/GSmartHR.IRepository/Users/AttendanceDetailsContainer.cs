using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.IRepository.Users
{
    public class AttendanceDetailsContainer
    {
        public int TotalRows { get; set; }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeIdNo { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
