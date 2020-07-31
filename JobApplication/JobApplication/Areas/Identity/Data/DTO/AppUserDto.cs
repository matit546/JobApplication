using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data.DTO
{
    public class AppUserDto
    {

        public string UserName { get; set; }
        public string Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(100,ErrorMessage ="Zbyt długa nazwa")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(100, ErrorMessage = "Zbyt długi nagłówek")]
        public string Headline { get; set; }
        public string Website { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Date)]
        public DateTime FoundingDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public CompanySize CompanySize { get; set; }
        [Required(ErrorMessage ="To pole jest wymagane")]
        public string ShortDescription { get; set; }
        public string Descrption { get; set; }
        public string Categories { get; set; }
        public string Address { get; set; }
        public string VideoUrl { get; set; }
        public string BackgroundImage { get; set; }
        public string Gallery { get; set; }
        public string FacebookProfile { get; set; }
        [PersonalData]
        public string TwitterProfile { get; set; }
        public string YoutubeProfile { get; set; }
        public string VimeoProfile { get; set; }
        public string LinkedinProfile { get; set; }

    }
    public enum CompanySize
    {
        Duże, Małe, Mikro, Średnie
    }
}

