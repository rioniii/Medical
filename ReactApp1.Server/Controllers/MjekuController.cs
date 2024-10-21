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
    public class MjekuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MjekuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mjeku
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mjeku>>> GetMjeket()
        {
            return await _context.Mjeket
                .Include(m => m.User)           // Include related User data
                .Include(m => m.Terminet)       // Include related Termini (Appointments)
                .Include(m => m.Historiks)      // Include related Historiks (History)
                .ToListAsync();
        }

        // GET: api/Mjeku/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mjeku>> GetMjeku(int id)
        {
            var mjeku = await _context.Mjeket
                .Include(m => m.User)           // Include related User data
                .Include(m => m.Terminet)       // Include related Termini (Appointments)
                .Include(m => m.Historiks)      // Include related Historiks (History)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mjeku == null)
            {
                return NotFound();
            }

            return mjeku;
        }

        // PUT: api/Mjeku/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMjeku(int id, Mjeku mjeku)
        {
            if (id != mjeku.Id)
            {
                return BadRequest();
            }

            _context.Entry(mjeku).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MjekuExists(id))
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

        // POST: api/Mjeku
        [HttpPost]
        public async Task<ActionResult<Mjeku>> PostMjeku(Mjeku mjeku)
        {
            // Make sure to add foreign key relations (User)
            var user = await _context.Users.FindAsync(mjeku.UserId);
            if (user == null)
            {
                return BadRequest("Invalid UserId provided.");
            }

            _context.Mjeket.Add(mjeku);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMjeku", new { id = mjeku.Id }, mjeku);
        }

        // DELETE: api/Mjeku/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMjeku(int id)
        {
            var mjeku = await _context.Mjeket.FindAsync(id);
            if (mjeku == null)
            {
                return NotFound();
            }

            _context.Mjeket.Remove(mjeku);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MjekuExists(int id)
        {
            return _context.Mjeket.Any(e => e.Id == id);
        }
    }
}
