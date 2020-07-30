using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplication.Pagination;
using JobApplication.Models;
using Microsoft.AspNetCore.Identity;
using JobApplication.Areas.Identity.Data;

namespace JobApplication.Areas.Jobs.Controllers
{
    [Area("Jobs")]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public  JobsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var joboffers = from j in _context.JobOffers
                                select j;
            int pageSize = 4;
            return View(await PaginationList<JobOffer>.CreateAsync(joboffers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await _context.JobOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }

        public async Task<IActionResult> Applied(OfferApplied offerApplied, int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            offerApplied.OfferId = id;
            offerApplied.UserId = user.Id;
            _context.OffersApplied.Add(offerApplied);
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
