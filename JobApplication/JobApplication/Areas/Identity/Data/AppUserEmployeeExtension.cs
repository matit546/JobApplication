using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class AppUserEmployeeExtension
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public bool IsShowing { get; set; }
        public string CVFile { get; set; }
        public string Languages { get; set; }
        public string Award { get; set; }
        public string Year { get; set; }
        public string DescriptionAward { get; set; }
        public string DegreeSchool { get; set; }
        public string SchoolName { get; set; }
        public string DateStartEndSchool { get; set; }
        public string DescriptionSchool { get; set; }
        public string DegreeCompany { get; set; }
        public string CompanyName { get; set; }
        public string DateStartEndComapny { get; set; }
        public string DescriptionCompany { get; set; }
        public string DegreeSkills { get; set; }
        public int Percentage { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
