using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.IRepository.Users
{
    public class UserDetailsContainer
    {
        public int TotalRows { get; set; }
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Rolename { get; set; }

        public bool IsActive { get; set; }
    }
}
