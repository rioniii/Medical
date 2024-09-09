using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepartController : ControllerBase
    {

        public readonly AppDBContext _context;

        public RepartController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRepartet()
        {
            var repartet = await _context.Reparti.ToListAsync();
            return Ok(repartet);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRepartById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID");
            }

            var repart = await _context.Reparti.FindAsync(id);
            if (repart == null)
            {
                return NotFound();
            }

            return Ok(repart);
        }

        [HttpPost]
        public async Task<ActionResult<List<Repart>>> AddReparti(Repart Repartet)
        {
            _context.Reparti.Add(Repartet);
            await _context.SaveChangesAsync();

            return Ok(await _context.Reparti.ToListAsync());
        }


        [HttpPatch]
        [Route("UpdateReparti/{id}")]
        public async Task<Repart> UpdateReparti(Repart obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete]
        public async Task<ActionResult<List<Repart>>> DeleteReparti(int id)
        {
            var dbRepartin = await _context.Reparti.FindAsync(id);
            if (dbRepartin == null)
                return NotFound("Reparti wasn't found");

            _context.Reparti.Remove(dbRepartin);

            await _context.SaveChangesAsync();

            return Ok(await _context.Reparti.ToListAsync()); ;
        }
    }
}


