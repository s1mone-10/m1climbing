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
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Climbing/User
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

        // GET: Climbing/MyCompletedRoutes/Create
        public IActionResult Create()
        {
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Name");
            return View();
        }

        // POST: Climbing/MyCompletedRoutes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,Date,Comment,ProposedGrade,PrivateNote")] UserCompletedRoute userCompletedRoute)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                userCompletedRoute.UserId = user.Id;
                _context.Add(userCompletedRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Name", userCompletedRoute.RouteId);
            return View(userCompletedRoute);
        }

        // GET: Climbing/MyCompletedRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var userCompletedRoute = await _context.UserCompletedRoutes
                .FirstOrDefaultAsync(u => u.Id == id && u.UserId == user.Id);

            if (userCompletedRoute == null)
                return NotFound();

            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Name", userCompletedRoute.RouteId);
            return View(userCompletedRoute);
        }

        // POST: Climbing/MyCompletedRoutes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RouteId,Date,Comment,ProposedGrade,PrivateNote")] UserCompletedRoute userCompletedRoute)
        {
            var user = await _userManager.GetUserAsync(User);
            var existing = await _context.UserCompletedRoutes
                .FirstOrDefaultAsync(u => u.Id == id && u.UserId == user.Id);

            if (existing == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                existing.RouteId = userCompletedRoute.RouteId;
                existing.Date = userCompletedRoute.Date;
                existing.Comment = userCompletedRoute.Comment;
                existing.ProposedGrade = userCompletedRoute.ProposedGrade;
                existing.PrivateNote = userCompletedRoute.PrivateNote;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Name", userCompletedRoute.RouteId);
            return View(userCompletedRoute);
        }

        // GET: Climbing/MyCompletedRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var userCompletedRoute = await _context.UserCompletedRoutes
                .Include(u => u.Route)
                .FirstOrDefaultAsync(u => u.Id == id && u.UserId == user.Id);

            if (userCompletedRoute == null)
                return NotFound();

            return View(userCompletedRoute);
        }

        // POST: Climbing/MyCompletedRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userCompletedRoute = await _context.UserCompletedRoutes
                .FirstOrDefaultAsync(u => u.Id == id && u.UserId == user.Id);

            if (userCompletedRoute != null)
            {
                _context.UserCompletedRoutes.Remove(userCompletedRoute);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}