using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace agenda.Models
{
    public partial class Broker
    {
        
        public Broker()
        {
            Appointments = new HashSet<Appointment>();
        }
        [Key]
        public int IdBroker { get; set; }
        [Required(ErrorMessage = "Veuillez saisir un nom.")]
        [Display(Name = "Nom")]
        public string Lastname { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un prénom.")]
        [Display(Name = "Prénom")]
        public string Firstname { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un mail.")]
        [Display(Name = "eMail")]
        [EmailAddress(ErrorMessage = "La saisie n'est pas une adress mail.")]
        public string Mail { get; set; } = null!;
        [Required(ErrorMessage = "Veuillez saisir un numéro de téléphone.")]
        [Display(Name = "Téléphone")]
        [Phone(ErrorMessage = "La saisie n'est pas un numéro de téléphone.")]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
