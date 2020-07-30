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
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(40, ErrorMessage = "Tytuł nie może być dłuższy niż 40 znaków")]
        public string Title { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Location { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TypeOfJob TypeOfJob { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Range(1, 20000, ErrorMessage = "Wartość maksymalna to 200000")]
        [DataType(DataType.Currency)]
        public decimal PaymentMin { get; set; }
        [Range(1, 200000, ErrorMessage = "Wypłata musi mieć wartość przynajmniej 1")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public decimal PaymentMax { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublicationTime { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Category { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Skills { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateTime Deadline { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Description { get; set; }
        public string ChooseTheCurrency { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Languages { get; set; }
        public string CompanyNameOffer { get; set; }
        public string WorkingTime { get; set; }
        public bool IsFeatured { get; set; }
        public string UserId { get; set; }
        public string PhotoCompanyOffer  { get; set; }
    }
    public enum TypeOfJob
    {
        Kontrakt, Freelancer, Część_etatu, Pełny_etat, Praca_czasowa, Praktyka, Wolontariat
    }
}
