using GSmartHR.Core.Domain;

namespace GSmartHR.Core.Domain.Users
{
    public class UserRole: BaseActivity
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
