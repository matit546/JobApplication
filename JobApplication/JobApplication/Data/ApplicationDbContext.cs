using System;
using System.Collections.Generic;
using System.Text;
using JobApplication.Models;
using JobApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<AppUserEmployeeExtension> AppUserEmployeeExtensions { get; set; }
        public DbSet<AwardsEmployee> AwardsEmployees { get; set; }
        public DbSet<EducationEmployee> EducationEmployees { get; set; }
        public DbSet<ExperiencesEmployee> ExperiencesEmployees { get; set; }
        public DbSet<SkillsEmployee> SkillsEmployees { get; set; }
        public DbSet<OfferApplied> OffersApplied { get; set; }
    }
}
