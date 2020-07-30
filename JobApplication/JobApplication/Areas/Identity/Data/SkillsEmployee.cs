using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class SkillsEmployee
    {
        public int Id { get; set; }
        public string Degree     { get; set; }
        public int Percentage { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
