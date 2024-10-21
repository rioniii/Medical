using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SherbimiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SherbimiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sherbimi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sherbimi>>> GetSherbimet()
        {
            return await _context.Sherbimet
                .Include(s => s.Faturat)  // Include related invoices (Faturat)
                .ToListAsync();
        }

        // GET: api/Sherbimi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sherbimi>> GetSherbimi(int id)
        {
            var sherbimi = await _context.Sherbimet
                .Include(s => s.Faturat)  // Include related invoices (Faturat)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sherbimi == null)
            {
                return NotFound();
            }

            return sherbimi;
        }

        // PUT: api/Sherbimi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSherbimi(int id, Sherbimi sherbimi)
        {
            if (id != sherbimi.Id)
            {
                return BadRequest();
            }

            _context.Entry(sherbimi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SherbimiExists(id))
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

        // POST: api/Sherbimi
        [HttpPost]
        public async Task<ActionResult<Sherbimi>> PostSherbimi(Sherbimi sherbimi)
        {
            _context.Sherbimet.Add(sherbimi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSherbimi", new { id = sherbimi.Id }, sherbimi);
        }

        // DELETE: api/Sherbimi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSherbimi(int id)
        {
            var sherbimi = await _context.Sherbimet.FindAsync(id);
            if (sherbimi == null)
            {
                return NotFound();
            }

            _context.Sherbimet.Remove(sherbimi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SherbimiExists(int id)
        {
            return _context.Sherbimet.Any(e => e.Id == id);
        }
    }
}
