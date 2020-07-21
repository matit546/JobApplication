using JobApplication.Areas.Identity.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data.ViewModels
{
    public class CandidateViewModel
    {
        public AppUserDto appUserDto { get; set; }
        public AppUserEmployeeExtension AppUserEmployeeExtension { get; set; }
        public ICollection<AwardsEmployee> AwardsEmployee { get; set; }
        public ICollection<EducationEmployee> EducationEmployee  { get; set; }
        public ICollection<ExperiencesEmployee> ExperiencesEmployee { get; set; }
        public ICollection<SkillsEmployee> SkillsEmployee  { get; set; }
        
    }
}
