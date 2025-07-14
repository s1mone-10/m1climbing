using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using m1project.Areas.Climbing.Models;
using m1project.Data;

namespace m1project.Areas.Climbing.Controllers
{
    [Route("api/[area]/[controller]")]
    [Area("Climbing")]
    [ApiController]
    public class ClimbingApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClimbingApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Climbing/Climbingapi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crag>>> GetCrag()
        {
            return await _context.Crag.ToListAsync();
        }

        // GET: api/Climbing/Climbingapi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crag>> GetCrag(int id)
        {
            var crag = await _context.Crag.FindAsync(id);

            if (crag == null)
            {
                return NotFound();
            }

            return crag;
        }

        // PUT: api/Climbing/Climbingapi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrag(int id, Crag crag)
        {
            if (id != crag.Id)
            {
                return BadRequest();
            }

            _context.Entry(crag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CragExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Climbingapi/Climbing
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Crag>> PostCrag(Crag crag)
        {
            _context.Crag.Add(crag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrag", new { id = crag.Id }, crag);
        }

        // DELETE: api/Climbing/Climbingapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrag(int id)
        {
            var crag = await _context.Crag.FindAsync(id);
            if (crag == null)
            {
                return NotFound();
            }

            _context.Crag.Remove(crag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Climbing/Climbingapi/GetSectors?cragId=5
        [HttpGet("GetSectors")]
        public async Task<ActionResult<IEnumerable<object>>> GetSectors(int cragId)
        {
            var sectors = await _context.Sector
                .Where(s => s.CragId == cragId)
                .Select(s => new { value = s.Id, text = s.Name })
                .ToListAsync();

            return Ok(sectors);
        }


        private bool CragExists(int id)
        {
            return _context.Crag.Any(e => e.Id == id);
        }
    }
}
