using JobApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)        
                {
                    _db.Database.Migrate();
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (_db.Roles.Any(r => r.Name == SD.AdminRole)) 
            {
                return;
            }

            _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.EmployerRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.CandidateRole)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            },"Haslo1!").GetAwaiter().GetResult();

            IdentityUser user = await _db.Users.FirstOrDefaultAsync(e => e.Email == "admin@gmail.com");

            await _userManager.AddToRoleAsync(user, SD.AdminRole);
        }
    }
}
