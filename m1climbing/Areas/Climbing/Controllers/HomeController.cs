using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace m1climbing.Areas.Climbing.Controllers
{
    [Area("Climbing")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Crags");
        }
    }
}
