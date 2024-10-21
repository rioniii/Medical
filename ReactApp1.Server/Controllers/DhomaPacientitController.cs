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
    public class DhomaPacientitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DhomaPacientitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DhomaPacientit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DhomaPacientit>>> GetDhomaPacientit()
        {
            return await _context.DhomaPacienteve
                .Include(dp => dp.Pacienti)   // Include the Pacienti related data
                .Include(dp => dp.Dhoma)      // Include the Dhoma related data
                .ToListAsync();
        }

        // GET: api/DhomaPacientit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DhomaPacientit>> GetDhomaPacientit(int id)
        {
            var dhomaPacientit = await _context.DhomaPacienteve
                .Include(dp => dp.Pacienti)
                .Include(dp => dp.Dhoma)
                .FirstOrDefaultAsync(dp => dp.Id == id);

            if (dhomaPacientit == null)
            {
                return NotFound();
            }

            return dhomaPacientit;
        }

        // PUT: api/DhomaPacientit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDhomaPacientit(int id, DhomaPacientit dhomaPacientit)
        {
            if (id != dhomaPacientit.Id)
            {
                return BadRequest();
            }

            _context.Entry(dhomaPacientit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DhomaPacientitExists(id))
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

        // POST: api/DhomaPacientit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DhomaPacientit>> PostDhomaPacientit(DhomaPacientit dhomaPacientit)
        {
            _context.DhomaPacienteve.Add(dhomaPacientit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDhomaPacientit", new { id = dhomaPacientit.Id }, dhomaPacientit);
        }

        // DELETE: api/DhomaPacientit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDhomaPacientit(int id)
        {
            var dhomaPacientit = await _context.DhomaPacienteve.FindAsync(id);
            if (dhomaPacientit == null)
            {
                return NotFound();
            }

            _context.DhomaPacienteve.Remove(dhomaPacientit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DhomaPacientitExists(int id)
        {
            return _context.DhomaPacienteve.Any(e => e.Id == id);
        }
    }
}
