using agenda.Db;
using agenda.Models;
using Microsoft.AspNetCore.Mvc;

namespace agenda.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DbConnect _db;

        public CustomerController(DbConnect db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Appel la vue du formulaire d'ajout d'un nouveau client.
        public IActionResult AddCustomer()
        {
            
            return View();
        }
        //Si la saisie est valide, ajout un nouveau client à la Base de données.
        [HttpPost]
        [ValidateAntiForgeryToken]//attaque de type cross--site
        public IActionResult AddCustomer(Customer newCustomer)
        {
            ModelState.Remove("Appointment");
            if (ModelState.IsValid)
            {
                _db.Customers.Add(newCustomer);
                if (_db.SaveChanges() > 0)
                {
                    TempData["success"] = "Le client a bien été ajouté.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["failure"] = "Une erreur c'est produite.";
                }
            }
            else
            {
                TempData["failure"] = "Probléme de saisie dans le formulaire.";
                return View(newCustomer);
            }
            return View(newCustomer);
        }
        //Renvoie la vue liste de clients
        public IActionResult ListCustomers()
        {
            List<Customer> customerslist = _db.Customers.ToList();
            customerslist = customerslist.OrderBy(o => o.Lastname).ToList();
            return View(customerslist);
        }
        //Appel la vue contenant les informations détaillés du client sélectioner via l'id passé en paramètre.
        public IActionResult ProfilCustomer(int id)
        {
            Customer customer = _db.Customers.Find(id);
            return View(customer);
        }
        //Appel la vue du formulaire de modification d'un client. 
        public IActionResult EditCustomer(int id)
        {
            Customer customerToEdit = _db.Customers.Find(id);
            if (customerToEdit == null || id == 0)
            {
                return NotFound();
            }
            return View(customerToEdit);
        }
        //Modifie les informations du client dans la base de données. 
        [HttpPost]
        [ValidateAntiForgeryToken]//attaque de type cross--site
        public IActionResult EditCustomer(Customer editCustomer, int id)
        {
            editCustomer.IdCustomer = id;
            ModelState.Remove("Appointment");
            if (ModelState.IsValid)
            {
                _db.Customers.Update(editCustomer);
                _db.SaveChanges();
                TempData["success"] = "Le profil a bien été modifié.";
                return RedirectToAction("ListCustomers");
            }
            TempData["failure"] = "Une erreur c'est produite lors de la modification du profil.";
            return View(editCustomer);
        }
        //Appel la vue de confirmation de suupression du client
        public IActionResult DeleteCustomer(int id)
        {
            Customer customer = _db.Customers.Find(id);
            return View(customer);
        }
        //Supprime le client
        [HttpPost]
        public IActionResult DeleteCustomer(Customer toDeletCustomer, int id)
        {
            toDeletCustomer.IdCustomer = id;
            ModelState.Remove("Appointment");
            if (ModelState.IsValid)
            {
                _db.Customers.Remove(toDeletCustomer);
                if (_db.SaveChanges() > 0)
                {
                    TempData["success"] = "Le client a bien été supprimé.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["failure"] = "Une erreur c'est produite.";
                }
            }
            return View(toDeletCustomer);
        }
    }
}
