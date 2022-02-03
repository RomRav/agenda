using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            
        }
        [Key]
        public int IdAppointment { get; set; }
        [Required(ErrorMessage = "Veuillez saisir l'heure du rendez-vous.")]
        [Display(Name = "Heure du rendez-vous")]
        public DateTime DateHour { get; set; }
        [Required(ErrorMessage = "Veuillez saisir le sujet.")]
        [Display(Name = "Sujet")]
        public string Subject { get; set; } = null!;
        [Display(Name ="Courtier")]
        public int IdBroker { get; set; } = 0;
        [Display(Name = "Client")]
        public int IdCustomer { get; set; } = 0;
        [ForeignKey("IdBroker")]
        public virtual Broker IdBrokerNavigation { get; set; } = null!;
        [ForeignKey("IdCustomer")]
        public virtual Customer IdCustomerNavigation { get; set; } = null!;
    }
}
