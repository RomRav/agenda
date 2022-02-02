using Microsoft.AspNetCore.Mvc;

namespace agenda.Controllers
{
    public class AppointmentController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
