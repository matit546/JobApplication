using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class AppUser: IdentityUser
    {
        [PersonalData]
        public string CompanyName { get; set; }
        [PersonalData]
        public string Headline { get; set; }
        [PersonalData]
        public string Website { get; set; }
        [PersonalData]
        public DateTime FoundingDate { get; set; }
        [PersonalData]
        public CompanySize CompanySize { get; set; }
        [PersonalData]
        public string ShortDescription { get; set; }
        [PersonalData]
        public string Descrption { get; set; }


    }
    public enum CompanySize
    {
        Duże, Małe, Mikro, Średnie
    }
}
