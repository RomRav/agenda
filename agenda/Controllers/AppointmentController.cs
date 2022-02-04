using agenda.Db;
using agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace agenda.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DbConnect _db;
        public AppointmentController(DbConnect db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Affiche la vue du formulaire de création d'un rendez-vous.
        public IActionResult AddAppointment(int id = 0)
        {
                ViewData["idBroker"] = id.ToString();
                     
            //Récupération de la liste des courtier
            IEnumerable<Broker> brokers = _db.Brokers;
            ViewData["brokersList"] = brokers;
            //Récupération de la liste des clients
            IEnumerable<Customer> customers = _db.Customers;
            ViewData["customersList"] = customers;

            return View();
        }
        //Enregistre dans la base de données le rendez-vous.
        [HttpPost]
        public IActionResult AddAppointment(Appointment newAppointment, int id)
        {
            string idCustomer = Request.Form["Client"];
            string idBroker = Request.Form["Courtier"];
            
            //List<Appointment> appointments = Appointment.GetAppointmentsList();
            if (id == 0)
            {
                newAppointment.IdBroker = Convert.ToInt32(idBroker);
            }
            else
            {
                newAppointment.IdBroker = Convert.ToInt32(id);  
            }
            newAppointment.IdCustomer = Convert.ToInt32(idCustomer);
            //Récupération de la liste des rendez-vous et verification que le créneau horaire du rdv à enregistré dans la BD n'est pas déjà occupé. 
            bool isOk = Appointment.CheckAvalableAppointmentTime(newAppointment);
            if (isOk)
            {
                _db.Add(newAppointment);
                if (_db.SaveChanges() > 0)
                {
                    TempData["success"] = "Le rendez-vous a bien été enregistré.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["failure"] = "Une erreur c'est produite.";
                    return RedirectToAction("AddAppointment", "Appointment");
                }
            }
            else
            {
                TempData["failure"] = "L'heure du rendez-vous est déjà prise.";
                return RedirectToAction("AddAppointment", "Appointment");
            }
        }
        //Renvoie la vue détaillé d'un rendez-vous
        public IActionResult DetailAppointment(int id)
        {
            Appointment appointment = Appointment.GetAppointmentsById(id);
            return View(appointment);
        }
        //Renvoie la vue de supprission d'un rendez-vous
        public IActionResult DeleteAppointment(int id)
        {
            return View();
        }

    }
}
