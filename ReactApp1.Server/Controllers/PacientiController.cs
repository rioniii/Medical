using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    //[Authorize]    
    [Route("api/[controller]")]
    [ApiController]
    public class PacientiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PacientiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Pacienti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pacienti>>> GetPacientet()
        {
            return await _context.Pacientet
                .Include(p => p.User)           // Include related User data
                .Include(p => p.Terminet)       // Include related Termini (Appointments)
                .Include(p => p.Historiks)      // Include related Historiks (History)
                .ToListAsync();
        }

        // GET: api/Pacienti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pacienti>> GetPacienti(int id)
        {
            var pacienti = await _context.Pacientet
                .Include(p => p.User)           // Include related User data
                .Include(p => p.Terminet)       // Include related Termini (Appointments)
                .Include(p => p.Historiks)      // Include related Historiks (History)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacienti == null)
            {
                return NotFound();
            }

            return pacienti;
        }

        // PUT: api/Pacienti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacienti(int id, Pacienti pacienti)
        {
            if (id != pacienti.Id)
            {
                return BadRequest();
            }

            _context.Entry(pacienti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientiExists(id))
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

        // POST: api/Pacienti
        [HttpPost]
        public async Task<ActionResult<Pacienti>> PostPacienti(Pacienti pacienti)
        {
            // Make sure to add foreign key relations (User)
            var user = await _context.Users.FindAsync(pacienti.UserId);
            if (user == null)
            {
                return BadRequest("Invalid UserId provided.");
            }

            _context.Pacientet.Add(pacienti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPacienti", new { id = pacienti.Id }, pacienti);
        }

        // DELETE: api/Pacienti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacienti(int id)
        {
            var pacienti = await _context.Pacientet.FindAsync(id);
            if (pacienti == null)
            {
                return NotFound();
            }

            _context.Pacientet.Remove(pacienti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacientiExists(int id)
        {
            return _context.Pacientet.Any(e => e.Id == id);
        }
    }
}
