

using System.Collections.Generic;

namespace GSmartHR.Core
{
    /// <summary>
    /// Represents work context
    /// </summary>
    public interface IWorkContext
    {
        bool IsAuthenticated { get; }
        UserModel CurrentUser { get; }
        bool IsInRole(string userType);
    }
}
