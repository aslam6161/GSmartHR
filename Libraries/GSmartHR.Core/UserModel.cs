using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Core
{
    [Serializable]
    public class UserModel 
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
    }
}
