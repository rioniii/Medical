using Microsoft.AspNetCore.Authorization;
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
    public class FaturaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FaturaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fatura

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetFaturas()
        {
            return await _context.Faturat
                .Include(f => f.Pacienti) // Including related Pacienti
                .Include(f => f.Sherbimi) // Including related Sherbimi
                .ToListAsync();
        }

        // GET: api/Fatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetFatura(int id)
        {
            var fatura = await _context.Faturat
                .Include(f => f.Pacienti)
                .Include(f => f.Sherbimi)
                .FirstOrDefaultAsync(f => f.Id.Equals(id));

            if (fatura == null)
            {
                return NotFound();
            }

            return fatura;
        }

        // PUT: api/Fatura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFatura(int id, Fatura fatura)
        {
            if (!(id.Equals(fatura.Id)))
            {
                return BadRequest();
            }

            _context.Entry(fatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaturaExists(id))
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

        // POST: api/Fatura
        [HttpPost]
        public async Task<ActionResult<Fatura>> PostFatura(Fatura fatura)
        {
            _context.Faturat.Add(fatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFatura), new { id = fatura.Id }, fatura);
        }

        // DELETE: api/Fatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFatura(int id)
        {
            var fatura = await _context.Faturat.FindAsync(id);
            if (fatura == null)
            {
                return NotFound();
            }

            _context.Faturat.Remove(fatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FaturaExists(int id)
        {
            return _context.Faturat.Any(e => e.Id.Equals(id));
        }
    }
}
