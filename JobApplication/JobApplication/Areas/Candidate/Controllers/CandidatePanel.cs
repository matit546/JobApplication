using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Areas.Candidate.Controllers
{
    [Area("Candidate")]
    [Authorize(Roles =SD.CandidateRole)]
    public class CandidatePanel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
