using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOffers.ToListAsync());
        }
    }
}
