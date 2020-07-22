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
        public List<AwardsEmployee> AwardsEmployee { get; set; }
        public List<EducationEmployee> EducationEmployee  { get; set; }
        public List<ExperiencesEmployee> ExperiencesEmployee { get; set; }
        public List<SkillsEmployee> SkillsEmployee  { get; set; }
        
    }
}
