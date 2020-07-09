using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class EducationEmployee
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string SchoolName { get; set; }
        public string DateStartEnd { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
