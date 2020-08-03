using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class OfferApplied
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("JobOffer")]
        public int OfferId { get; set; }
        public JobOffer JobOffer { get; set; }

    }
}
