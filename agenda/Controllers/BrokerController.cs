using agenda.Db;
using agenda.Models;
using Microsoft.AspNetCore.Mvc;

namespace agenda.Controllers
{
    public class BrokerController : Controller
    {
        private readonly DbConnect _db;
        public BrokerController(DbConnect db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Appel la vue du formulaire d'ajout d'un nouveau courtier.
        public IActionResult AddBroker()
        {
            return View();
        }
        //Si la saisie est valide, ajout un nouveau courtier à la Base de données.
        [HttpPost]
        [ValidateAntiForgeryToken]//attaque de type cross--site
        public IActionResult AddBroker(Broker newBroker)
        {
           
            if (ModelState.IsValid)
            {
                _db.Brokers.Add(newBroker);

                if (_db.SaveChanges() > 0)
                {
                    TempData["success"] = "Le courtier a bien été ajouté.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["fail"] = "Une erreur c'est produite.";
                }
            }  
            return View(newBroker);
        }
        //Récupére et envoie à la vue la liste des courtiers
        [Route("Profil")]
        public IActionResult ListBrokers()
        {
            //Les instructions qui recuperent mes courtiers que je passerai a ma vue ici
            IEnumerable<Broker> brokers = _db.Brokers;
            return View(brokers);
        }
        //Récupére les informations du courtier selectionné grace à l'Id et les envoie à la vue ProfilBroker.
        public IActionResult ProfilBrokers(int id)
        {
            Broker selectedBroker = _db.Brokers.Find(id);
            return View(selectedBroker);
        }
        //Appel la vue du formulaire de modification d'un courtier. 
        public IActionResult EditBroker(int id)
        {
            Broker brokerToEdit = _db.Brokers.Find(id);
            if (brokerToEdit == null || id == 0)
            {
                return NotFound();
            }
            return View(brokerToEdit);
        }
        //Appel la vue du formulaire de modification d'un courtier. 
        [HttpPost]
        [ValidateAntiForgeryToken]//attaque de type cross--site
        public IActionResult EditBroker(Broker editBroker, int id)
        {
            
            editBroker.IdBroker = id;
            if (ModelState.IsValid)
            {
                _db.Brokers.Update(editBroker);
                _db.SaveChanges();
                TempData["success"] = "Le profil a bien été modifié.";
                return RedirectToAction("ListBrokers");
            }
            TempData["fail"] = "Une erreur c'est produite lors de la modification du profil.";
            return View(editBroker);
        }

    }
}
