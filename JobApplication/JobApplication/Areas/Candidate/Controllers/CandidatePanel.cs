using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobApplication.Areas.Identity.Data;
using JobApplication.Areas.Identity.Data.DTO;
using JobApplication.Data;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        public async Task<IActionResult> EditProfile()      // Get current User data
        {
            var user = _userManager.GetUserId(HttpContext.User);

            var userExtensionExists = _context.AppUserEmployeeExtensions.FirstOrDefault(u => u.UserId == user);
            if (userExtensionExists == null)
            {
                var userApp = await _userManager.GetUserAsync(HttpContext.User);

                AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(userApp);
                var json = JsonConvert.SerializeObject(appUserDto);
                return Json(json);
            }
            else
            {
                var userExtension = await _context.AppUserEmployeeExtensions.Include(x => x.AppUser).FirstOrDefaultAsync();
                AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(userExtension.AppUser);
                var json = JsonConvert.SerializeObject(userExtension);
                return Json(json);
            }


        }
    }
}
