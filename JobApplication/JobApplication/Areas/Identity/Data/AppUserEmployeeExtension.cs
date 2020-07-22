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
        public IsShowing IsShowing { get; set; }
        public string CVFile { get; set; }
        public string Languages { get; set; }
        public string TypePreferes { get; set; }
        public string Levels { get; set; }
        public string Localization { get; set; }
        public decimal PaymentMinProfile { get; set; }
        public string ExperiencesProfile { get; set; }
        public string SkillsProfile { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
    public enum IsShowing
    {
        Tak, Nie
    }
}
