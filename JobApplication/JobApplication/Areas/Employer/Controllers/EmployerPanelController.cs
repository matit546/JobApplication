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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JobApplication.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = SD.EmployerRole)]

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
            var userId = _userManager.GetUserId(User);
            var jobOffers = await _context.JobOffers.Where(u => u.UserId == userId).ToListAsync();

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

        //Post EditProfile User and Adding/updating Image
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
                    return RedirectToAction("Index", "EmployerPanel", new { name = "Panel" });
                }

            }
            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.EmployerRole)]
        public async Task<IActionResult> CreateNewJobOffer([Bind("Title,Location,TypeOfJob,PaymentMin,PaymentMax,PublicationTime,Category," +
                "Skills,Deadline,Description,ChooseTheCurrency,Email,Languages,CompanyNameOffer,WorkingTime")] JobOffer jobOffer) //Add new Job Offer
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                String dateTime = DateTime.Now.ToShortDateString();
                jobOffer.PublicationTime = Convert.ToDateTime(dateTime);
                jobOffer.UserId = currentUser.Id;
                jobOffer.PhotoCompanyOffer = currentUser.BackgroundImage;
                jobOffer.CompanyNameOffer = currentUser.CompanyName;
                _context.Add(jobOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //Tempdata alert That smth went wrong
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> EditJobOffer(int? id)        //Get View for Edit JobOffer
        {
            if (id == null)
            {
                return NotFound();
            }

            var joboffer = await _context.JobOffers.FindAsync(id);

            if (joboffer == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(HttpContext.User);

            if (userId == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (joboffer.UserId != userId)
            {
                return Unauthorized("Nie możesz zedytować tej oferty!");
            }

            return View(joboffer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJobOffer(JobOffer jobOffer)        //Post Edit Jod Offer
        {
            var userdb = await _userManager.GetUserAsync(User);
            if (userdb == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var jobofferfromDb = await _context.JobOffers.FindAsync(jobOffer.Id);
            if (jobofferfromDb == null || jobofferfromDb.UserId != userdb.Id)
            {
                return Unauthorized("NIe możesz zedytować tej oferty!");
            }
            if (ModelState.IsValid)
            {
                jobofferfromDb.Category = jobOffer.Category;
                jobofferfromDb.ChooseTheCurrency = jobOffer.ChooseTheCurrency;
                jobofferfromDb.CompanyNameOffer = jobOffer.CompanyNameOffer;
                jobofferfromDb.Deadline = jobOffer.Deadline;
                jobofferfromDb.Description = jobOffer.Description;
                jobofferfromDb.Email = jobOffer.Email;
                jobofferfromDb.Languages = jobOffer.Languages;
                jobofferfromDb.Location = jobOffer.Location;
                jobofferfromDb.PaymentMax = jobOffer.PaymentMax;
                jobofferfromDb.PaymentMin = jobOffer.PaymentMin;
                jobofferfromDb.Title = jobOffer.Title;
                jobofferfromDb.TypeOfJob = jobOffer.TypeOfJob;
                jobofferfromDb.WorkingTime = jobOffer.WorkingTime;
                jobofferfromDb.PhotoCompanyOffer = userdb.BackgroundImage;
                jobofferfromDb.Skills = jobOffer.Skills;

                _context.Update(jobofferfromDb);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Pomyślnie zedytowano ofertę pracy";
                return RedirectToAction("Index", "EmployerPanel", "?name=MojeOferty");
            }
            return View(jobOffer);
        }


        public async Task<IActionResult> DeleteJobOffer(int? id)        //Get View for Edit JobOffer
        {
            if (id == null)
            {
                return NotFound();
            }

            var userdb = await _userManager.GetUserAsync(User);
            if (userdb == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var jobofferfromDb = await _context.JobOffers.FindAsync(id);

            if (jobofferfromDb == null || jobofferfromDb.UserId != userdb.Id)
            {
                return Unauthorized("NIe możesz usunąć tej oferty!");
            }
            _context.JobOffers.Remove(jobofferfromDb);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Pomyślnie usunięto ofertę pracy";
            return RedirectToAction("Index", "EmployerPanel", "?name=MojeOferty");
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
        [HttpGet]
        public async Task<IActionResult> CandidatesApplied()
        {
            var user = _userManager.GetUserId(HttpContext.User);

            var jobs = _context.JobOffers.Where(x => x.UserId == user).ToList();

            var offers = _context.OffersApplied.ToList();

            var result = offers.Where(y => jobs.Any(z => z.Id == y.OfferId)).ToList();

            var userCandidate = _userManager.Users.ToList();

           // var jobstoShow = jobs.Where(x => result.Any(z => z.OfferId == x.Id));

            var resultCandidates = userCandidate.Where(x => result.Any(y => y.UserId == x.Id));

            var appUserDto = _mapper.Map<IEnumerable<AppUser>, List<AppUserDto>>(resultCandidates);
   
            var json = JsonConvert.SerializeObject(appUserDto);
            // var jsonTateusz = JsonConvert.SerializeObject(result);
            // var jsonMati = json + jsonTateusz;
            return Json(json);
        }
        [HttpGet]
        public async Task<IActionResult> ShowCandidateInformation(string id)  //View for information of the candiadate who applied to employer Joboffer
        {
            var userCandidate = _userManager.Users.FirstOrDefault(x => x.UserName== id);

            CandidateViewModel candidateViewModel = new CandidateViewModel
            {
                appUserDto = _mapper.Map<AppUser, AppUserDto>(userCandidate),
                AppUserEmployeeExtension = await _context.AppUserEmployeeExtensions.FirstOrDefaultAsync(x => x.UserId == userCandidate.Id),
                AwardsEmployee = await _context.AwardsEmployees.Where(x => x.UserId == userCandidate.Id).ToListAsync(),
                EducationEmployee =await _context.EducationEmployees.Where(x => x.UserId == userCandidate.Id).ToListAsync(),
                ExperiencesEmployee = await _context.ExperiencesEmployees.Where(x => x.UserId == userCandidate.Id).ToListAsync(),
                SkillsEmployee = await _context.SkillsEmployees.Where(x => x.UserId == userCandidate.Id).ToListAsync()
            };

            return View(candidateViewModel);
        }

        [HttpGet]
        public ActionResult GetCv(string id)        //action for view in ShowCandidateInformation it shows cv of user who applied to offer
        {
            if (id == null)
            {
                return NotFound();
            }
            var cvfilepath = _context.AppUserEmployeeExtensions.FirstOrDefault(i => i.CVFile == id);
            if (cvfilepath == null)
            {
                return NotFound("brak cv");
            }
            var uploadPath = _iWebHost.WebRootPath + @"\files\" + cvfilepath.CVFile;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", cvfilepath.CVFile);

            if (!System.IO.File.Exists(path))
            {
                return NotFound("Nie znaleziono takiego pliku");
            }

            string filePath = "~/files/" + cvfilepath.CVFile;
            Response.Headers.Add($"Content-Disposition", "inline; filename=" + cvfilepath.CVFile);
            return File(filePath, "application/pdf");
        }
    }
    }

