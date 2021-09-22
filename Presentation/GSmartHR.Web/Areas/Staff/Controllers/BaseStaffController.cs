using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GSmartHR.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Policy = "StaffHandler")]
    public class BaseStaffController : Controller
    {
        public bool IsEditMode(Guid? id)
        {
            return id.HasValue && Guid.Empty != id.Value;
        }
    }
}