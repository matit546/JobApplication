using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class AwardsEmployee
    {
        public int Id { get; set; }
        public string Award { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
