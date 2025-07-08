using Microsoft.AspNetCore.Mvc;

namespace m1project.Areas.Climbing.Controllers
{
    [Area("Climbing")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Crags");
        }
    }
}
