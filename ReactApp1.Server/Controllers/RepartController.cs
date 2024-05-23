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
        public async Task<ActionResult<List<Repart>>> GetAllRepartet()
        {
            var Reparti = await _context.Reparti.FindAsync();
            return Ok(Reparti);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Repart>>> GetRepartin(int id)
        {
            var Reparti = await _context.Reparti.ToListAsync();
            if (Reparti == null)
                return NotFound("Reparti wasn't found");
            return Ok(Reparti);


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


