using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminetController : ControllerBase
    {
        public readonly AppDBContext _context;

        public TerminetController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Terminet>>> GetAllTerminet()
        {
            var TERMIN = await _context.Termini.ToListAsync();

            return Ok(TERMIN);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Terminet>>> GetTerminet(int id)
        {
            var OKOK = await _context.Termini.FindAsync(id);
            if (OKOK == null)
                return NotFound("Role not found");
            return Ok(OKOK);
        }

        [HttpPost]
        public async Task<ActionResult<List<Terminet>>> AddTermini(Terminet Term)
        {
            _context.Termini.Add(Term);
            await _context.SaveChangesAsync();

            return Ok(await _context.Termini.ToListAsync()); ;
        }



        [HttpPatch]
        [Route("UpdateTermini/{id}")]
        public async Task<Terminet> UpdateTermini(Terminet term)
        {
            _context.Entry(term).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return term;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Terminet>>> DeleteTermini(int id)
        {
            var dbTerminet = await _context.Termini.FindAsync(id);
            if (dbTerminet == null)
                return NotFound("Termini not found");

            _context.Termini.Remove(dbTerminet);

            await _context.SaveChangesAsync();

            return Ok(await _context.Termini.ToListAsync()); ;
        }
    }
}
