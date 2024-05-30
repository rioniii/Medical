using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PershkrimiController : ControllerBase
    {

        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PershkrimiController(AppDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pershkrimi>>> GetAllPershkrimi()
        {
            var Pershkrimi = await _context.Pershkrimi.ToListAsync();

            return Ok(Pershkrimi);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pershkrimi>>> GetPershkrimi(int id)
        {
            var Pershkrimi = await _context.Pershkrimi.FindAsync(id);
            if (Pershkrimi == null)
                return NotFound("Pershkrimi not found");
            return Ok(Pershkrimi);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pershkrimi>>> AddPershkrimi(Pershkrimi Pershkrimi)
        {
            _context.Pershkrimi.Add(Pershkrimi);
            await _context.SaveChangesAsync();

            return Ok(await _context.Pershkrimi.ToListAsync());
        }


        [HttpPatch]
        [Route("UpdatePershkrimi/{id}")]
        public async Task<Pershkrimi> UpdatePershkrimi(Pershkrimi obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete]
        public async Task<ActionResult<List<Pershkrimi>>> DeletePershkrimi(int id)
        {
            var dbPershkrimi = await _context.Pershkrimi.FindAsync(id);
            if (dbPershkrimi == null)
                return NotFound("Pershkrimi not found");

            _context.Pershkrimi.Remove(dbPershkrimi);

            await _context.SaveChangesAsync();

            return Ok(await _context.Pershkrimi.ToListAsync()); ;
        }
    }
}

