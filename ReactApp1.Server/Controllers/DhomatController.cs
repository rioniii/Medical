using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhomatController : ControllerBase
    {

        public readonly AppDBContext _context;

        public DhomatController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dhomat>>> GetAllDhomatt()
        {
            var Dhomat = await _context.Dhomat.ToListAsync();

            return Ok(Dhomat);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Dhomat>>> GetDhomat(int id)
        {
            var Dhomat = await _context.Dhomat.FindAsync(id);
            if (Dhomat == null)
                return NotFound("Dhomat not found");
            return Ok(Dhomat);
        }

        [HttpPost]
        public async Task<ActionResult<List<Dhomat>>> AddDhomat(Dhomat Dhomat)
        {
            _context.Dhomat.Add(Dhomat);
            await _context.SaveChangesAsync();

            return Ok(await _context.Dhomat.ToListAsync());
        }


        [HttpPatch]
        [Route("UpdateDhomat/{id}")]
        public async Task<Dhomat> UpdateDhomat(Dhomat obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete]
        public async Task<ActionResult<List<Dhomat>>> DeleteDhomat(int id)
        {
            var dbDhomat = await _context.Dhomat.FindAsync(id);
            if (dbDhomat == null)
                return NotFound("Dhomat not found");

            _context.Dhomat.Remove(dbDhomat);

            await _context.SaveChangesAsync();

            return Ok(await _context.Dhomat.ToListAsync()); ;
        }
    }
}


