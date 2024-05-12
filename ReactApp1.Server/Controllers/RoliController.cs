using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoliController : ControllerBase
    {
        public readonly AppDBContext _context;

        public RoliController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Roles>>> GetAllRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return Ok(roles);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Roles>>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound("Role not found");
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<List<Roles>>> AddRole(Roles role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync()); ;
        }



        [HttpPatch]
        [Route("UpdateRole/{id}")]
        public async Task<Roles> UpdateRole(Roles objRole)
        {
            _context.Entry(objRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objRole;
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