using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using m1project.Areas.Climbing.Models;
using m1project.Data;
using Microsoft.AspNetCore.Authorization;
using m1project.Constants;

namespace m1project.Areas.Climbing.Controllers
{
    [Area("Climbing")]
    public class CragsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CragsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Climbing/Crags
        public async Task<IActionResult> Index()
        {
            // TODO Copilot suggestion: The use of `ToListAsync()` can lead to performance issues if the dataset is large. Consider using pagination or filtering to limit the number of records retrieved.
            return View(await _context.Crag.ToListAsync());
        }

        // GET: Climbing/Crags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crag = await _context.Crag
                .Include(c => c.Sectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crag == null)
            {
                return NotFound();
            }

            return View(crag);
        }

        // GET: Climbing/Crags/Create
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Climbing/Crags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Region")] Crag crag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crag);
        }

        // GET: Climbing/Crags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crag = await _context.Crag.FindAsync(id);
            if (crag == null)
            {
                return NotFound();
            }
            return View(crag);
        }

        // POST: Climbing/Crags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Region")] Crag crag)
        {
            if (id != crag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CragExists(crag.Id))
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
            return View(crag);
        }

        // GET: Climbing/Crags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crag = await _context.Crag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crag == null)
            {
                return NotFound();
            }

            return View(crag);
        }

        // POST: Climbing/Crags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crag = await _context.Crag.FindAsync(id);
            if (crag != null)
            {
                _context.Crag.Remove(crag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CragExists(int id)
        {
            return _context.Crag.Any(e => e.Id == id);
        }
    }
}
