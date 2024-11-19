using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TerminiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Termini
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Termini>>> GetTerminet()
        {
            return await _context.Terminet
                .Include(t => t.Mjeku)
                .Include(t => t.Pacienti)
                .ToListAsync();
        }

        // GET: api/Termini/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Termini>> GetTermini(int id)
        {
            var termini = await _context.Terminet
                .Include(t => t.Mjeku)
                .Include(t => t.Pacienti)
                .FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (termini == null)
            {
                return NotFound();
            }

            return termini;
        }

        // PUT: api/Termini/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermini(int id, Termini termini)
        {
            if (!(id.Equals(termini.Id)))
            {
                return BadRequest();
            }

            _context.Entry(termini).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminiExists(id))
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

        // POST: api/Termini
        [HttpPost]
        public async Task<ActionResult<Termini>> PostTermini(Termini termini)
        {
            _context.Terminet.Add(termini);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTermini", new { id = termini.Id }, termini);
        }

        // DELETE: api/Termini/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermini(int id)
        {
            var termini = await _context.Terminet.FindAsync(id);
            if (termini == null)
            {
                return NotFound();
            }

            _context.Terminet.Remove(termini);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TerminiExists(int id)
        {
            return _context.Terminet.Any(e => e.Id.Equals(id));
        }
    }
}
