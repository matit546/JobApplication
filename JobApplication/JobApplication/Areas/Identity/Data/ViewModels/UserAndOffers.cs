using JobApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data.ViewModels
{
    public class UserAndOffers
    {
        public AppUser appUser {get;set;}
        public JobOffer jobOffer { get; set; }

    }
}
