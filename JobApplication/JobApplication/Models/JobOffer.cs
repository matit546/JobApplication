using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class JobOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string TypeOfJob { get; set; }
        public decimal PaymentMin { get; set; }
        public decimal PaymentMax { get; set; }
        [Required]
        public DateTime PublicationTime { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        public string ChooseTheCurrency { get; set; }
    }
}
