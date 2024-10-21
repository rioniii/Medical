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
    public class DhomaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DhomaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dhoma
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dhoma>>> GetDhoma()
        {
            return await _context.Dhomat.Include(d => d.DhomaPacienteve).ToListAsync();
        }

        // GET: api/Dhoma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dhoma>> GetDhoma(int id)
        {
            var dhoma = await _context.Dhomat.Include(d => d.DhomaPacienteve).FirstOrDefaultAsync(d => d.Id == id);

            if (dhoma == null)
            {
                return NotFound();
            }

            return dhoma;
        }

        // PUT: api/Dhoma/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDhoma(int id, Dhoma dhoma)
        {
            if (id != dhoma.Id)
            {
                return BadRequest();
            }

            _context.Entry(dhoma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DhomaExists(id))
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

        // POST: api/Dhoma
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dhoma>> PostDhoma(Dhoma dhoma)
        {
            _context.Dhomat.Add(dhoma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDhoma", new { id = dhoma.Id }, dhoma);
        }

        // DELETE: api/Dhoma/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDhoma(int id)
        {
            var dhoma = await _context.Dhomat.FindAsync(id);
            if (dhoma == null)
            {
                return NotFound();
            }

            _context.Dhomat.Remove(dhoma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DhomaExists(int id)
        {
            return _context.Dhomat.Any(e => e.Id == id);
        }
    }
}
