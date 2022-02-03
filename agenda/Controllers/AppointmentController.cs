using agenda.Db;
using agenda.Models;
using Microsoft.AspNetCore.Mvc;
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
            if (id == 0)
            {
                ViewData["idBroker"] = "";
            }
            else
            {
                ViewData["idBroker"] = id.ToString();
            }
            
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
        public IActionResult AddAppointment(Appointment newAppointment)
        {
            
            string idCustomer = Request.Form["Client"];
            string idBroker = Request.Form["Courtier"];
            newAppointment.IdCustomer = Convert.ToInt32(idCustomer);
            newAppointment.IdBroker = Convert.ToInt32(idBroker);
            _db.Add(newAppointment);
            _db.SaveChanges();
            return RedirectToAction("Home", "Index");
        }

    }
}
