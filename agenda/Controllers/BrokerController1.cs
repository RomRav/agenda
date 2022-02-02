using Microsoft.AspNetCore.Mvc;

namespace agenda.Controllers
{
    public class BrokerController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
