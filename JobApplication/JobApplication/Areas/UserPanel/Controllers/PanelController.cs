using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobApplication.Areas.Identity.Data;
using JobApplication.Data;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Areas.Identity.Data.ViewModels;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace JobApplication.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _iWebHost;
        public PanelController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHost)
        {
            _context = context;
            _userManager = userManager;
            _iWebHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
        
       
        //Get User Data
        [HttpGet]
        public async Task<IActionResult> EditProfile(AppUser appUser)
        {
 
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            var  json = JsonConvert.SerializeObject(user);
            return Json(json);
            


        }

        //Edit User Profile
        [HttpPost]
        [Authorize(Roles = SD.EmployerRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfilePost([Bind("Id,CompanyName,Email,UserName,Headline,Website,FoundingDate,CompanySize,ShortDescription,Descrption" +
            ",Categories,Address,VideoUrl,Gallery,FacebookProfile,TwitterProfile,YoutubeProfile,VimeoProfile,LinkedinProfile")] AppUser appUser, IFormFile file)
        {

            if (file != null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user.BackgroundImage != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", user.BackgroundImage);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                if (file != null && file.Length > 0)
                {
                    var imagePath = @"\images\";
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
                    appUser.BackgroundImage= fileName;
                    //_context.Update(appUser);
                    await _userManager.UpdateAsync(appUser);
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.Descrption = appUser.Descrption;
                user.FacebookProfile = appUser.FacebookProfile;
                user.Address = appUser.Address;
                user.BackgroundImage = appUser.BackgroundImage;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction(nameof(Index));


        }
        public  PartialViewResult GetEditPartialView()
        {
            return PartialView("_EditPartialView");

        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =SD.EmployerRole)]
        public async Task<IActionResult> CreateNewJobOffer([Bind("Title,Location,TypeOfJob,PaymentMin,PaymentMax,PublicationTime,Category,Skills,Deadline,Description,ChooseTheCurrency")] JobOffer jobOffer)
        {
            jobOffer.PublicationTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(jobOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index));
        }


    }

}
