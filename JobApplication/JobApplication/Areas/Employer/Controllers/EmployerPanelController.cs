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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JobApplication.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles =SD.EmployerRole)]
   
        public class EmployerPanelController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IWebHostEnvironment _iWebHost;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ILogger<Controllers.EmployerPanelController> _logger;
            private readonly IMapper _mapper;

            public EmployerPanelController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHost
                , ILogger<Controllers.EmployerPanelController> logger, SignInManager<AppUser> signInManager, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _iWebHost = webHost;
                _signInManager = signInManager;
                _logger = logger;
                _mapper = mapper;
            }

            public IActionResult Index()            //Default VIew for Employer
            {
                 return View();
            }

            //Get User Data
            [HttpGet]
            public async Task<IActionResult> GetJobOffers()         //Get Job offers Current User and return JSON
            {
                var jobOffers = await _context.JobOffers.ToListAsync();
                if (jobOffers == null)
                {
                    return NotFound();
                }

                var json = JsonConvert.SerializeObject(jobOffers);
                return Json(json);
            }


            //Get User Data
            [HttpGet]

            public async Task<IActionResult> EditProfile()      // Get current User data
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                
                if (user == null)
                {
                    return NotFound();
                }

                AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(user);
                var json = JsonConvert.SerializeObject(appUserDto);
                return Json(json);

            }

            //Post User changes in data
            [HttpPost]
            [Authorize(Roles = SD.EmployerRole)]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ProfilePost([Bind("Id,CompanyName,Email,Headline,Website,FoundingDate,CompanySize,ShortDescription,Descrption" +
            ",Categories,Address,VideoUrl,Gallery,FacebookProfile,TwitterProfile,YoutubeProfile,VimeoProfile,LinkedinProfile,PhoneNumber")] AppUserDto appUserDto, IFormFile file)
            {
                if (ModelState.IsValid)
                {
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

                    updateUser.PhoneNumber = appUserDto.PhoneNumber;
                    updateUser.Address = appUserDto.Address;
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
                    return RedirectToAction("Index", "EmployerPanel", new {name= "Panel" } );
                }

                }
                return RedirectToAction(nameof(Index));


            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = SD.EmployerRole)]
            public async Task<IActionResult> CreateNewJobOffer([Bind("Title,Location,TypeOfJob,PaymentMin,PaymentMax,PublicationTime,Category,Skills,Deadline,Description,ChooseTheCurrency")] JobOffer jobOffer) //Add new Job Offer
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



            [HttpGet]
            public async Task<IActionResult> ResetPassword()        //Get View for ResetPassword
            {

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ResetPassword(ResetPasswordModel Input)        //Post Change Password to current User
        {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);
                _logger.LogInformation($"Uzytkownik  { user.UserName }  zmienil swoje haslo ");
                TempData["ReturnUrl"] = "Hasło zostało zmienione";
                return View();
            }
        }
    }

