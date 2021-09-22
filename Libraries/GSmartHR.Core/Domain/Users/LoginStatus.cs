using System;
using System.Collections.Generic;
using System.Text;

namespace GSmartHR.Core.Domain.Users
{
    public enum LoginStatus
    {
        Successful = 1,
        UserNotExist = 2,
        WrongPassword = 3,
        NotActive = 4,
        Deleted = 5,
        NotRegistered = 6,
    }
}
