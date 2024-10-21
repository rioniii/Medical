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
    public class JWTController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JWTController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JWT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JWT>>> GetJWTs()
        {
            return await _context.JWTs
                .Include(j => j.User)  // Include the related User
                .ToListAsync();
        }

        // GET: api/JWT/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JWT>> GetJWT(int id)
        {
            var jwt = await _context.JWTs
                .Include(j => j.User)  // Include the related User
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jwt == null)
            {
                return NotFound();
            }

            return jwt;
        }

        // PUT: api/JWT/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJWT(int id, JWT jwt)
        {
            if (id != jwt.Id)
            {
                return BadRequest();
            }

            _context.Entry(jwt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JWTExists(id))
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

        // POST: api/JWT
        [HttpPost]
        public async Task<ActionResult<JWT>> PostJWT(JWT jwt)
        {
            _context.JWTs.Add(jwt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJWT", new { id = jwt.Id }, jwt);
        }

        // DELETE: api/JWT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJWT(int id)
        {
            var jwt = await _context.JWTs.FindAsync(id);
            if (jwt == null)
            {
                return NotFound();
            }

            _context.JWTs.Remove(jwt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JWTExists(int id)
        {
            return _context.JWTs.Any(e => e.Id == id);
        }
    }
}
