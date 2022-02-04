using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace agenda.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
        }
        [Key]
        public int IdCustomer { get; set; }
        [Required(ErrorMessage = "Veuillez saisir un nom.")]
        [Display(Name = "Nom client")]
        public string Lastname { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un prénom.")]
        [Display(Name = "Prénom client")]
        public string Firstname { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un mail.")]
        [Display(Name = "eMail")]
        [EmailAddress(ErrorMessage ="La saisie n'est pas une adress mail.")]
        public string Mail { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un numéro de téléphone.")]
        [Display(Name = "Téléphone")]
        [Phone(ErrorMessage = "La saisie n'est pas un numéro de téléphone.")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un budget.")]
        [Display(Name = "Budget")]
        public int Budget { get; set; }

        //public virtual Appointment Appointment { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
