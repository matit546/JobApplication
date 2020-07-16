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

namespace JobApplication.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public PanelController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        
       
        //Get User Data
        [HttpGet]
        public async Task<IActionResult> EditProfile()
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
            ",Categories,Address,VideoUrl,Gallery,FacebookProfile,TwitterProfile,YoutubeProfile,VimeoProfile,LinkedinProfile")] AppUser appUser)
        {
    

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
