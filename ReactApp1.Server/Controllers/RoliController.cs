using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoliController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoliController(AppDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Roles>>> GetAllRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return Ok(roles);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound("Role not found");
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Roles>> AddRole(Roles role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }



        [HttpPatch]
        [Route("UpdateRole/{id}")]
        public async Task<Roles> UpdateRole(int id, Roles objRole)
        {
            var roleToUpdate = await _context.Roles.FindAsync(id);
            if (roleToUpdate == null)
                return NotFound("Role not found");

            _context.Entry(roleToUpdate).CurrentValues.SetValues(objRole);
            await _context.SaveChangesAsync();
            return roleToUpdate;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Roles>>> DeleteRole(int id)
        {
            var dbRole = await _context.Roles.FindAsync(id);
            if (dbRole == null)
                return NotFound("Role not found");

            _context.Roles.Remove(dbRole);

            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync()); ;
        }
    }
}
