using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplication.Pagination;
using JobApplication.Models;

namespace JobApplication.Areas.Jobs.Controllers
{
    [Area("Jobs")]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public  JobsController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
