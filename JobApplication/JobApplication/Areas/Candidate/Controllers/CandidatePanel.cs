using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobApplication.Areas.Identity.Data;
using JobApplication.Areas.Identity.Data.DTO;
using JobApplication.Areas.Identity.Data.ViewModels;
using JobApplication.Data;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JobApplication.Areas.Candidate.Controllers
{
    [Area("Candidate")]
    [Authorize(Roles = SD.CandidateRole)]
    public class CandidatePanel : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _iWebHost;
        private readonly SignInManager<AppUser> _signInManager;
        // private readonly ILogger<CandidatePanel> _logger;
        private readonly IMapper _mapper;
        public CandidatePanel(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHost
                , SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _iWebHost = webHost;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string name = null)        //Get Candidate User Data
        {
            if (name != "Panel")
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"nie znaleziono uzytkownika z takim ID '{_userManager.GetUserId(User)}'.");
            }
            AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(user);
            return View(appUserDto);

        }

        [HttpGet]
        public async Task<IActionResult> EditProfileEmployee()      // Get current User data
        {
            //var user = _userManager.GetUserId(HttpContext.User);

            //var userExtensionExists = _context.AppUserEmployeeExtensions.FirstOrDefault(u => u.UserId == user);
            //if (userExtensionExists == null)
            //{
            //    var userApp = await _userManager.GetUserAsync(HttpContext.User);

            //    AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(userApp);
            //    var json = JsonConvert.SerializeObject(appUserDto);
            //    return Json(json);
            //}
            //else
            //{
            //    var userExtension = await _context.AppUserEmployeeExtensions.Include(x => x.AppUser).FirstOrDefaultAsync();
            //    AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(userExtension.AppUser);
            //    var json = JsonConvert.SerializeObject(userExtension);
            //    return Json(json);
            //}
            var user = _userManager.GetUserId(HttpContext.User);
            var userApp = await _userManager.GetUserAsync(HttpContext.User);
            CandidateViewModel candidateViewModel = new CandidateViewModel
            {
                appUserDto = _mapper.Map<AppUser, AppUserDto>(userApp),
            AppUserEmployeeExtension = await _context.AppUserEmployeeExtensions.Include(x => x.AppUser).FirstOrDefaultAsync(x=>x.UserId==user),
                AwardsEmployee = (ICollection<AwardsEmployee>)_context.AwardsEmployees.FirstOrDefault(x => x.UserId == user),
                EducationEmployee = (ICollection<EducationEmployee>)_context.EducationEmployees.FirstOrDefault(x => x.UserId == user),
                ExperiencesEmployee = (ICollection<ExperiencesEmployee>)_context.ExperiencesEmployees.FirstOrDefault(x => x.UserId == user),
                SkillsEmployee = (ICollection<SkillsEmployee>)_context.SkillsEmployees.FirstOrDefault(x => x.UserId == user)
            };
            return View(candidateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("Id,UserName,Email,Headline,Website,FoundingDate,CompanySize,ShortDescription,Descrption" +
            ",Categories,Address,VideoUrl,Gallery,FacebookProfile,TwitterProfile,YoutubeProfile,VimeoProfile,LinkedinProfile,PhoneNumber")] CandidateViewModel candidateViewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (candidateViewModel.AppUserEmployeeExtension.Id == 0)
                {
                    // .ADD
                }

                var updateUser = await _userManager.GetUserAsync(HttpContext.User);
                if (file != null)
                {
                    if (updateUser == null)
                    {
                        return NotFound();
                    }
                    if (updateUser.BackgroundImage != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", updateUser.BackgroundImage);
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
                        updateUser.BackgroundImage = fileName;
                    }
                }

                updateUser.PhoneNumber = candidateViewModel.appUserDto.PhoneNumber;
                updateUser.Address = candidateViewModel.appUserDto.CompanyName;
                //updateUser.BackgroundImage = appUserDto.BackgroundImage;
                updateUser.Categories = candidateViewModel.appUserDto.Categories;
                updateUser.CompanyName = candidateViewModel.appUserDto.Email;
                updateUser.Descrption = candidateViewModel.appUserDto.Headline;

                //updateUser.Gallery = appUserDto.AppUser.Gallery;
                //updateUser.FoundingDate = appUserDto.FoundingDate;
                //updateUser.ShortDescription = appUserDto.ShortDescription;
                //updateUser.Headline = appUserDto.Headline;
                //updateUser.LinkedinProfile = appUserDto.LinkedinProfile;
                //updateUser.TwitterProfile = appUserDto.TwitterProfile;
                //updateUser.VideoUrl = appUserDto.VideoUrl;
                //updateUser.VimeoProfile = appUserDto.VimeoProfile;
                //updateUser.Website = appUserDto.Website;
                //updateUser.YoutubeProfile = appUserDto.YoutubeProfile;

                var result = await _userManager.UpdateAsync(updateUser);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(Index));


        }
    }
}
