using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Areas.Employer.Controllers
{
    [Area("Employer")]

    public class EmployerController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)          // if user is logged in we redirect him to Employer controller
            {
                return RedirectToAction("Index", "EmployerPanel", "?name=Panel");
            }
            return View();
        }
    }
}
