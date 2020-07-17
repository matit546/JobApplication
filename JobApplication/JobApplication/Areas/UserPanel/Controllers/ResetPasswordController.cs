using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class ResetPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
