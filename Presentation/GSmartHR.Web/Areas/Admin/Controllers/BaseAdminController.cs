using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GSmartHR.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminHandler")]
    public class BaseAdminController : Controller
    {
        public bool IsEditMode(Guid? id)
        {
            return id.HasValue && Guid.Empty != id.Value;
        }
    }
}