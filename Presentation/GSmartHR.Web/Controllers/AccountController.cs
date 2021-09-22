using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GSmartHR.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return RedirectToAction("Login", "User",new {area="", returnUrl= ReturnUrl });
        }
    }
}