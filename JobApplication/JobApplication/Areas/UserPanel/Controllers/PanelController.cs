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
using JobApplication.Areas.Identity.Data.DTO;

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
        public async Task<IActionResult> EditProfile()
        {
 
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return NotFound();
            }

            var appUserDto = new AppUserDto
            {
                Id=user.Id,
                PhoneNumber=user.PhoneNumber,
                Address = user.Address,
                BackgroundImage = user.BackgroundImage,
                Categories = user.Categories,
                CompanyName = user.CompanyName,
                CompanySize = (Identity.Data.DTO.CompanySize)user.CompanySize,
                Descrption = user.Descrption,
                Email = user.Email,
                FacebookProfile = user.FacebookProfile,
                Gallery = user.Gallery,
                FoundingDate = user.FoundingDate,
                ShortDescription = user.ShortDescription,
                Headline = user.Headline,
                LinkedinProfile = user.LinkedinProfile,
                TwitterProfile = user.TwitterProfile,
                VideoUrl = user.VideoUrl,
                VimeoProfile = user.VimeoProfile,
                Website = user.Website,
                YoutubeProfile = user.YoutubeProfile
            };

            var  json = JsonConvert.SerializeObject(appUserDto);
            return Json(json);
            
        }

        //Edit User Profile
        [HttpPost]
        [Authorize(Roles = SD.EmployerRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfilePost([Bind("Id,CompanyName,Email,Headline,Website,FoundingDate,CompanySize,ShortDescription,Descrption" +
            ",Categories,Address,VideoUrl,Gallery,FacebookProfile,TwitterProfile,YoutubeProfile,VimeoProfile,LinkedinProfile")] AppUserDto appUserDto, IFormFile file)
        {

            if (file != null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return NotFound();
                }
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
                    user.BackgroundImage= fileName;
                    //_context.Update(appUser);
                    await _userManager.UpdateAsync(user);
                }
            }

            if (ModelState.IsValid)
            {
                var updateUser = await _userManager.GetUserAsync(HttpContext.User);

                updateUser.PhoneNumber = appUserDto.PhoneNumber;
                updateUser.Address = appUserDto.Address;
                updateUser.BackgroundImage = appUserDto.BackgroundImage;
                updateUser.Categories = appUserDto.Categories;
                updateUser.CompanyName = appUserDto.CompanyName;
                updateUser.CompanySize = (Identity.Data.CompanySize)appUserDto.CompanySize;
                updateUser.Descrption = appUserDto.Descrption;
                updateUser.Email = appUserDto.Email;
                updateUser.FacebookProfile = appUserDto.FacebookProfile;
                updateUser.Gallery = appUserDto.Gallery;
                updateUser.FoundingDate = appUserDto.FoundingDate;
                updateUser.ShortDescription = appUserDto.ShortDescription;
                updateUser.Headline = appUserDto.Headline;
                updateUser.LinkedinProfile = appUserDto.LinkedinProfile;
                updateUser.TwitterProfile = appUserDto.TwitterProfile;
                updateUser.VideoUrl = appUserDto.VideoUrl;
                updateUser.VimeoProfile = appUserDto.VimeoProfile;
                updateUser.Website = appUserDto.Website;
                updateUser.YoutubeProfile = appUserDto.YoutubeProfile;

                var result = await _userManager.UpdateAsync(updateUser);

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
