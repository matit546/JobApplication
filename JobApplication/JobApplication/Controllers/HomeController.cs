using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using JobApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using JobApplication.Areas.Identity.Data;

namespace JobApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _iWebHost;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _context = context;
            _iWebHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOffers.ToListAsync());
        }
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task <IActionResult> Privacy(IFormFile file, AppUser appUser)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\img\images\";
                var uploadPath = _iWebHost.WebRootPath + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var newFileName = Guid.NewGuid().ToString();
                var fileName = Path.GetFileName(newFileName + "." + file.FileName.Split(".")[1].ToLower());
                string fullPath = uploadPath + fileName;
                imagePath = imagePath + @"\";
                var filePath = @".." + Path.Combine(imagePath, fileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                appUser.BackgroundImage = fileName;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
