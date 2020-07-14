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
        public async Task<IActionResult> Index()
        {
      
            var user = await _userManager.GetUserAsync(HttpContext.User);
            UserAndOffers userAndOffers = new UserAndOffers()
            {
                jobOffer = null,
                appUser = await _userManager.GetUserAsync(HttpContext.User)
        };
       
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View();
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
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


    }

}
