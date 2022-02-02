using Microsoft.AspNetCore.Mvc;

namespace agenda.Controllers
{
    public class CustomerController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
