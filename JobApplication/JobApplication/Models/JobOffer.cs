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
        [StringLength(40, ErrorMessage = "Tytuł nie może być dłuższy niż 40 znaków")]
        public string Title { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string TypeOfJob { get; set; }
        [Range(1, 20000, ErrorMessage = "Wartość maksymalna to 200000")]
        [DataType(DataType.Currency)]
        public decimal PaymentMin { get; set; }
        [Range(1, 200000, ErrorMessage = "Wypłata musi mieć wartość przynajmniej 1")]
        [DataType(DataType.Currency)]
        public decimal PaymentMax { get; set; }
        [Required]
        public DateTime PublicationTime { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Skills { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        public string ChooseTheCurrency { get; set; }
    }
}
