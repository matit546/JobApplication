using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class OfferApplied
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int OfferId { get; set; }
        public JobOffer JobOffer { get; set; }

    }
}
