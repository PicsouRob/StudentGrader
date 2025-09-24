using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

