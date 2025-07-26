using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using m1climbing.Areas.Climbing.Models;
using m1climbing.Areas.Identity.Data;
using m1climbing.Data;

namespace m1climbing.Areas.Climbing.Controllers
{
    [Area("Climbing")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Climbing/Account
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var completedRoutes = await _context.UserCompletedRoutes
                .Include(u => u.Route)
                .Where(u => u.UserId == user.Id)
                .OrderByDescending(u => u.Date)
                .ToListAsync();
            return View(completedRoutes);
        }
    }
}