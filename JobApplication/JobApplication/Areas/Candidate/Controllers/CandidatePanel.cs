using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
using Microsoft.AspNetCore.Razor.Language;
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
        public  IActionResult Index(string name = null)        //Default View for Candidate
        {

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditProfileEmployee()      // Get current User data
        {
  
            var userApp = await _userManager.GetUserAsync(HttpContext.User);
            CandidateViewModel candidateViewModel = new CandidateViewModel
            {
                appUserDto = _mapper.Map<AppUser, AppUserDto>(userApp),
                AppUserEmployeeExtension =  _context.AppUserEmployeeExtensions.FirstOrDefault(x=>x.UserId==userApp.Id),
                AwardsEmployee = _context.AwardsEmployees.Where(x => x.UserId == userApp.Id).ToList(),
                EducationEmployee = _context.EducationEmployees.Where(x => x.UserId == userApp.Id).ToList(),
                ExperiencesEmployee = _context.ExperiencesEmployees.Where(x => x.UserId == userApp.Id).ToList(),
                SkillsEmployee = _context.SkillsEmployees.Where(x => x.UserId == userApp.Id).ToList()
            };
            if(candidateViewModel.AppUserEmployeeExtension == null)
            {
                candidateViewModel.AppUserEmployeeExtension = new AppUserEmployeeExtension();
            }
            var json = JsonConvert.SerializeObject(candidateViewModel);
            return Json(json);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfileEmployee(CandidateViewModel candidateViewModel, IFormFile file, IFormFile cvFile)
        {
            if (ModelState.IsValid)
            {
                var updateUser = await _userManager.GetUserAsync(HttpContext.User);
                // var objfromDb = await _context.EducationEmployees.Where(x => x.UserId == updateUser.Id).ToListAsync();
                //int i = 0;
                //foreach ( var exists in objfromDb)
                //{
                //    if (exists.Id != candidateViewModel.EducationEmployee[i].Id)
                //    {
                //        return NotFound();
                //    }
                //    i++;
                //}
                if (candidateViewModel.EducationEmployee != null)
                {
                    foreach (var item in candidateViewModel.EducationEmployee)
                    {
                        item.UserId = updateUser.Id;
                        if (item.Id == 0)
                        {
                            _context.Add(item);
                        }
                        else
                        {
                            _context.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                if (candidateViewModel.ExperiencesEmployee != null)
                {
                    foreach (var item in candidateViewModel.ExperiencesEmployee)
                    {
                        item.UserId = updateUser.Id;
                        if (item.Id == 0)
                        {
                            _context.Add(item);
                        }
                        else
                        {
                            _context.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                if (candidateViewModel.AwardsEmployee != null)
                {
                    foreach (var item in candidateViewModel.AwardsEmployee)
                    {
                        item.UserId = updateUser.Id;
                        if (item.Id == 0)
                        {
                            _context.Add(item);
                        }
                        else
                        {
                            _context.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                if (candidateViewModel.SkillsEmployee != null)
                {
                    foreach (var item in candidateViewModel.SkillsEmployee)
                    {
                        item.UserId = updateUser.Id;
                        if (item.Id == 0)
                        {
                            _context.Add(item);
                        }
                        else
                        {
                            _context.Update(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                if (cvFile != null)
                {
                    if (!(cvFile.ContentType == "application/pdf"))
                    {
                        return NotFound("TO nie jest plik PDF");
                    }
                    if (candidateViewModel.AppUserEmployeeExtension.CVFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", candidateViewModel.AppUserEmployeeExtension.CVFile);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    if (cvFile != null && cvFile.Length > 0)
                    {
                        var imagePath = @"\files\";
                        var uploadPath = _iWebHost.WebRootPath + imagePath;

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        var newFileName = Guid.NewGuid().ToString();
                        var fileName = Path.GetFileName(newFileName + "." + cvFile.FileName.Split(".")[1].ToLower());
                        string fullPath = uploadPath + fileName;
                        imagePath = imagePath + @"\";
                        var filePath = @".." + Path.Combine(imagePath, fileName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await cvFile.CopyToAsync(fileStream);
                        }
                        candidateViewModel.AppUserEmployeeExtension.CVFile = fileName;

                    }
                }
  
                candidateViewModel.AppUserEmployeeExtension.UserId = updateUser.Id;
                if (candidateViewModel.AppUserEmployeeExtension.Id == 0)
                {
                    _context.Add(candidateViewModel.AppUserEmployeeExtension);

                }else
                {
                    var AppUser = _context.AppUserEmployeeExtensions.Where(i => i.UserId == updateUser.Id);
                    _context.Update(candidateViewModel.AppUserEmployeeExtension);
                }
                await _context.SaveChangesAsync();

                if (file != null)
                {
                    if (updateUser == null)
                    {
                        return NotFound();
                    }
                    if (updateUser.BackgroundImage != null && updateUser.BackgroundImage != "defaultImage.jpg")
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
               
         
                //User Update
                updateUser.PhoneNumber = candidateViewModel.appUserDto.PhoneNumber;
                updateUser.Address = candidateViewModel.appUserDto.Address;
                updateUser.Categories = candidateViewModel.appUserDto.Categories;
                updateUser.CompanyName = candidateViewModel.appUserDto.CompanyName;
                updateUser.Descrption = candidateViewModel.appUserDto.Descrption;
                updateUser.Gallery = candidateViewModel.appUserDto.Gallery;
                updateUser.FoundingDate = candidateViewModel.appUserDto.FoundingDate;
                updateUser.ShortDescription = candidateViewModel.appUserDto.ShortDescription;
                updateUser.Headline = candidateViewModel.appUserDto.Headline;
                updateUser.LinkedinProfile = candidateViewModel.appUserDto.LinkedinProfile;
                updateUser.TwitterProfile = candidateViewModel.appUserDto.TwitterProfile;
                updateUser.VideoUrl = candidateViewModel.appUserDto.VideoUrl;
                updateUser.VimeoProfile = candidateViewModel.appUserDto.VimeoProfile;
                updateUser.Website = candidateViewModel.appUserDto.Website;
                updateUser.YoutubeProfile = candidateViewModel.appUserDto.YoutubeProfile;
                updateUser.FacebookProfile = candidateViewModel.appUserDto.FacebookProfile;
                updateUser.Email = candidateViewModel.appUserDto.Email;

                var result = await _userManager.UpdateAsync(updateUser);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(Index));


        }
        [HttpGet]
        public async Task<IActionResult> AppliedOffers()
        {
            var userApp = _userManager.GetUserId(HttpContext.User);
            var offerApplied = await _context.OffersApplied.Where(x=>x.UserId==userApp).ToListAsync();
            var jobOffers= await _context.JobOffers.ToListAsync();
            foreach (var job in offerApplied)
            {
                jobOffers = await _context.JobOffers.Where(y => y.Id == job.OfferId).ToListAsync();
            }

            if (jobOffers == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(jobOffers);
            return Json(json);
        }

        [HttpGet]
        public ActionResult GetPdf()
        {
            var userApp = _userManager.GetUserId(HttpContext.User);
            var cvfilepath = _context.AppUserEmployeeExtensions.FirstOrDefault(i => i.UserId == userApp);
            var uploadPath = _iWebHost.WebRootPath + @"\files\"+ cvfilepath.CVFile;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", cvfilepath.CVFile);

            if (!System.IO.File.Exists(path))
            {
                return NotFound("Nie znaleziono takiego pliku");
            }

            string filePath = "~/files/"+cvfilepath.CVFile;
            Response.Headers.Add($"Content-Disposition", "inline; filename="+cvfilepath.CVFile);
            return File(filePath, "application/pdf");
        }
    }
  
}
