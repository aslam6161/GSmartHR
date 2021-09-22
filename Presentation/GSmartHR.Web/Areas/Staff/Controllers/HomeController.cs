using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GSmartHR.Core.Domain;
using GSmartHR.Web.Infrastructure;
using GSmartHR.Web.Areas.Admin.Models;

namespace GSmartHR.Web.Areas.Staff.Controllers
{
   
    public class HomeController : BaseStaffController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}