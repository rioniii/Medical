using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturimiController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FaturimiController(AppDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Faturimi>>> GetAllFaturat()
        {
            var faturat = await _context.Faturat.ToListAsync();

            return Ok(faturat);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Faturimi>> GetFaturat(int id)
        {
            var fatura = await _context.Faturat.FindAsync(id);
            if (fatura == null)
                return NotFound("Fatura not found");
            return Ok(fatura);
        }

        [HttpPost]
        public async Task<ActionResult<Faturimi>> AddFaturat(Faturimi faturat)
        {
            _context.Faturat.Add(faturat);
            await _context.SaveChangesAsync();

            return Ok(faturat);
        }



        [HttpPatch]
        [Route("UpdateFatura/{id}")]
        public async Task<ActionResult<Faturimi>> UpdateFatura(int id, Faturimi faturat)
        {
            var faturaToUpdate = await _context.Faturat.FindAsync(id);
            if (faturaToUpdate == null)
                return NotFound("Fatura not found");

            _context.Entry(faturaToUpdate).CurrentValues.SetValues(faturat);
            await _context.SaveChangesAsync();
            return Ok(faturaToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Faturimi>> DeleteFaturat(int id)
        {
            var faturaToDelete = await _context.Faturat.FindAsync(id);
            if (faturaToDelete == null)
                return NotFound("Fatura not found");

            _context.Faturat.Remove(faturaToDelete);

            await _context.SaveChangesAsync();

            return Ok(faturaToDelete);
        }
    }
}
