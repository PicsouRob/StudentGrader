using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Grade()
        {
            return View();
        }

        public IActionResult Subject()
        {
            return View();
        }

        public IActionResult Student()
        {
            return View();
        }
    }
}

