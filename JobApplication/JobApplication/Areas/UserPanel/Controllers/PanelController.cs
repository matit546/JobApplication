﻿using System;
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
using Newtonsoft.Json;

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
        public IActionResult Index()
        {
            return View();
        }

       
        //Get User Data
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(user);
            return Json(json);

        }
  
        public  async Task<PartialViewResult> GetPartialView()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return PartialView("_PartialView",user);

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
