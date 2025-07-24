using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using m1climbing.Areas.Climbing.Models;
using m1climbing.Data;
using Microsoft.AspNetCore.Authorization;

namespace m1climbing.Areas.Climbing.Controllers
{
    [Area("Climbing")]
    public class UserCompletedRoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserCompletedRoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Climbing/UserCompletedRoutes
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
        public IActionResult Create()
        {
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Grade");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Grade", userCompletedRoute.RouteId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCompletedRoute.UserId);
            return View(userCompletedRoute);
        }

        // GET: Climbing/UserCompletedRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompletedRoute = await _context.UserCompletedRoutes.FindAsync(id);
            if (userCompletedRoute == null)
            {
                return NotFound();
            }
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Grade", userCompletedRoute.RouteId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCompletedRoute.UserId);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.Route, "Id", "Grade", userCompletedRoute.RouteId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCompletedRoute.UserId);
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

            return View(userCompletedRoute);
        }

        // POST: Climbing/UserCompletedRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCompletedRoute = await _context.UserCompletedRoutes.FindAsync(id);
            if (userCompletedRoute != null)
            {
                _context.UserCompletedRoutes.Remove(userCompletedRoute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCompletedRouteExists(int id)
        {
            return _context.UserCompletedRoutes.Any(e => e.Id == id);
        }
    }
}
