using m1climbing.Areas.Climbing.Models;
using m1climbing.Areas.Identity.Data;
using m1climbing.Constants;
using m1climbing.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m1climbing.Areas.Climbing.Controllers
{
    [Area("Climbing")]
    public class UserCompletedRoutesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCompletedRoutesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Climbing/UserCompletedRoutes
        [Authorize(Policy = Policies.ManageClimbingData)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserCompletedRoutes.Include(u => u.Route).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Climbing/UserCompletedRoutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompletedRoute = await _context.UserCompletedRoutes
                .Include(u => u.Route)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCompletedRoute == null)
            {
                return NotFound();
            }

            return View(userCompletedRoute);
        }

        // GET: Climbing/UserCompletedRoutes/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["UserId"] = user!.Id;
            ViewData["CragId"] = new SelectList(_context.Crag, "Id", "Name");

            return View();
        }

        // POST: Climbing/UserCompletedRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RouteId,Date,Comment,ProposedGrade,PrivateNote")] UserCompletedRoute userCompletedRoute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCompletedRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Account");
            }

            ViewData["UserId"] = userCompletedRoute.UserId;
            ViewData["CragId"] = new SelectList(_context.Crag, "Id", "Name");
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Name", userCompletedRoute.RouteId);
            return View(userCompletedRoute);
        }

        // GET: Climbing/UserCompletedRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompletedRoute = await _context.UserCompletedRoutes.Include(u => u.Route).FirstOrDefaultAsync(u => u.Id == id);
            if (userCompletedRoute == null)
            {
                return NotFound();
            }
            
            var user = await _userManager.GetUserAsync(User);
            
            if (userCompletedRoute.UserId != user!.Id && !User.IsInRole(Roles.Admin))
            {
                return Forbid(); // Ensures user can edit only their own records
            }

            ViewData["UserId"] = user!.Id;
            var cragId = userCompletedRoute.Route?.CragId;
            ViewData["CragId"] = new SelectList(_context.Crag, "Id", "Name", cragId);
            ViewData["RouteId"] = new SelectList(_context.Route.Where(r => r.CragId == cragId), "Id", "Name", userCompletedRoute.RouteId);
            return View(userCompletedRoute);
        }

        // POST: Climbing/UserCompletedRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RouteId,Date,Comment,ProposedGrade,PrivateNote")] UserCompletedRoute userCompletedRoute)
        {
            if (id != userCompletedRoute.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (userCompletedRoute.UserId != user!.Id && !User.IsInRole(Roles.Admin))
            {
                return Forbid(); // Ensures user can edit only their own records
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCompletedRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCompletedRouteExists(userCompletedRoute.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Account");
            }
            
            ViewData["UserId"] = user!.Id;
            ViewData["CragId"] = new SelectList(_context.Crag, "Id", "Name");
            return View(userCompletedRoute);
        }

        // GET: Climbing/UserCompletedRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompletedRoute = await _context.UserCompletedRoutes
                .Include(u => u.Route)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCompletedRoute == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (userCompletedRoute.UserId != user!.Id && !User.IsInRole(Roles.Admin))
            {
                return Forbid(); // Ensures user can edit only their own records
            }


            return View(userCompletedRoute);
        }

        // POST: Climbing/UserCompletedRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCompletedRoute = await _context.UserCompletedRoutes.FindAsync(id);

            var user = await _userManager.GetUserAsync(User);
            if (userCompletedRoute?.UserId != user!.Id && !User.IsInRole(Roles.Admin))
            {
                return Forbid(); // Ensures user can edit only their own records
            }

            if (userCompletedRoute != null)
            {
                _context.UserCompletedRoutes.Remove(userCompletedRoute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Account");
        }

        private bool UserCompletedRouteExists(int id)
        {
            return _context.UserCompletedRoutes.Any(e => e.Id == id);
        }
    }
}
