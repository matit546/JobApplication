using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class AppUser: IdentityUser
    {
        public string CompanyName { get; set; }
        public string Headline { get; set; }
        public string Website { get; set; }
        public DateTime FoundingDate { get; set; }
        public CompanySize CompanySize { get; set; }
        public string ShortDescription { get; set; }
        public string Descrption { get; set; }
        public string Categories { get; set; }
        public string Address { get; set; }
        public string VideoUrl { get; set; }
        public string BackgroundImage { get; set; }
        public string Gallery { get; set; }
        public string FacebookProfile { get; set; }
        public string TwitterProfile { get; set; }
        public string YoutubeProfile { get; set; }
        public string vimeoProfile { get; set; }
        public string LinkedinProfile { get; set; }




    }
    public enum CompanySize
    {
        Duże, Małe, Mikro, Średnie
    }
}
