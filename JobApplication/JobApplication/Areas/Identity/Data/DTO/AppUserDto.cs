using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data.DTO
{
    public class AppUserDto
    {

        [PersonalData]
        public string Id { get; set; }
        [PersonalData]
        public string PhoneNumber { get; set; }
        [PersonalData]
        [Required]
        public string Email { get; set; }

        [PersonalData]
        [Required]
        public string CompanyName { get; set; }

        [Required]
        [PersonalData]
        public string Headline { get; set; }
        [PersonalData]
        public string Website { get; set; }

        [PersonalData]
        [Required]
        public DateTime FoundingDate { get; set; }
        [PersonalData]
        [Required]
        public CompanySize CompanySize { get; set; }
        [PersonalData]
        [Required]
        public string ShortDescription { get; set; }
        [PersonalData]
        public string Descrption { get; set; }
        [PersonalData]
        public string Categories { get; set; }
        [PersonalData]
        public string Address { get; set; }
        [PersonalData]
        public string VideoUrl { get; set; }
        [PersonalData]
        public string BackgroundImage { get; set; }
        [PersonalData]
        public string Gallery { get; set; }
        [PersonalData]
        public string FacebookProfile { get; set; }
        [PersonalData]
        public string TwitterProfile { get; set; }
        [PersonalData]
        public string YoutubeProfile { get; set; }
        [PersonalData]
        public string VimeoProfile { get; set; }
        [PersonalData]
        public string LinkedinProfile { get; set; }

    }
    public enum CompanySize
    {
        Duże, Małe, Mikro, Średnie
    }
}

