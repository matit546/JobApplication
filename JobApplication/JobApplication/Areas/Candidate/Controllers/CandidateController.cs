using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Areas.Candidate.Controllers
{

    [Area("Candidate")]
    public class CandidateController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CandidateController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobOffers.ToListAsync());
        }
    }
}
