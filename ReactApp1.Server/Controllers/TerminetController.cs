using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminetController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TerminetController(AppDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Terminet>>> GetAllTerminet()
        {
            var TERMIN = await _context.Termini.ToListAsync();

            return Ok(TERMIN);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Terminet>> GetTerminet(int id)
        {
            var OKOK = await _context.Termini.FindAsync(id);
            if (OKOK == null)
                return NotFound("Terminet not found");
            return Ok(OKOK);
        }

        [HttpPost]
        public async Task<ActionResult<Terminet>> AddTermini(Terminet Term)
        {
            _context.Termini.Add(Term);
            await _context.SaveChangesAsync();

            return Ok(Term);
        }



        [HttpPatch]
        [Route("UpdateTermini/{id}")]
        public async Task<ActionResult<Terminet>> UpdateTermini(int id, Terminet term)
        {
            var terminToUpdate = await _context.Termini.FindAsync(id);
            if (terminToUpdate == null)
                return NotFound("Terminet not found");

            _context.Entry(terminToUpdate).CurrentValues.SetValues(term);
            await _context.SaveChangesAsync();
            return Ok(terminToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Terminet>> DeleteTermini(int id)
        {
            var terminToDelete = await _context.Termini.FindAsync(id);
            if (terminToDelete == null)
                return NotFound("Terminet not found");

            _context.Termini.Remove(terminToDelete);

            await _context.SaveChangesAsync();

            return Ok(terminToDelete);
        }
    }
}
