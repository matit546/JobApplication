using JobApplication.Areas.Identity.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data.ViewModels
{
    public class CandidateViewModel
    {
       public AppUserDto AppUserDto { get; set; }
       public AppUserEmployeeExtension AppUserEmployeeExtension { get; set; }
    }
}
