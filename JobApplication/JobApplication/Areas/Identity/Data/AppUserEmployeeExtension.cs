﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data
{
    public class AppUserEmployeeExtension
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public bool IsShowing { get; set; }
        public string CVFile { get; set; }
        public string Languages { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
