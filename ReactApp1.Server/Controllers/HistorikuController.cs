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
    public class HistorikuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistorikuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Historiku
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historiku>>> GetHistoriku()
        {
            return await _context.Historiks
                .Include(h => h.Mjeku)  // Include Mjeku (Doctor) details
                .Include(h => h.Pacienti)  // Include Pacienti (Patient) details
                .ToListAsync();
        }

        // GET: api/Historiku/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Historiku>> GetHistoriku(string id)
        {
            var historiku = await _context.Historiks
                .Include(h => h.Mjeku)
                .Include(h => h.Pacienti)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historiku == null)
            {
                return NotFound();
            }

            return Ok(historiku);
        }

        // PUT: api/Historiku/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoriku(string id, Historiku historiku)
        {
            if (id != historiku.Id)
            {
                return BadRequest();
            }

            _context.Entry(historiku).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorikuExists(id))
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

        // POST: api/Historiku
        [HttpPost]
        public async Task<ActionResult<Historiku>> PostHistoriku(Historiku historiku)
        {
            _context.Historiks.Add(historiku);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistoriku), new { id = historiku.Id }, historiku);
        }

        // DELETE: api/Historiku/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoriku(string id)
        {
            var historiku = await _context.Historiks.FindAsync(id);
            if (historiku == null)
            {
                return NotFound();
            }

            _context.Historiks.Remove(historiku);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorikuExists(string id)
        {
            return _context.Historiks.Any(e => e.Id == id);
        }
    }
}
