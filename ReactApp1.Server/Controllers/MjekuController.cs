using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MjekuController : ControllerBase
    {

        public readonly AppDBContext _context;

        public MjekuController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mjeku>>> GetAllMjekat()
        {
            var Mjekat = await _context.Mjekat.ToListAsync();

            return Ok(Mjekat);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Mjeku>>> GetMjekat(int id)
        {
            var Mjekaa = await _context.Mjekat.FindAsync(id);
            if (Mjekaa == null)
                return NotFound("Mjekat not found");
            return Ok(Mjekaa);
        }

        [HttpPost]
        public async Task<ActionResult<List<Mjeku>>> AddMjekat(Mjeku Mjekatt)
        {
            _context.Mjekat.Add(Mjekatt);
            await _context.SaveChangesAsync();

            return Ok(await _context.Mjekat.ToListAsync());
        }


        [HttpPatch]
        [Route("UpdateDhomat/{id}")]
        public async Task<Mjeku> UpdateMjekat(Mjeku obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete]
        public async Task<ActionResult<List<Mjeku>>> DeleteMjekat(int id)
        {
            var dbMjekat = await _context.Mjekat.FindAsync(id);
            if (dbMjekat == null)
                return NotFound("Dhomat not found");

            _context.Mjekat.Remove(dbMjekat);

            await _context.SaveChangesAsync();

            return Ok(await _context.Mjekat.ToListAsync()); ;
        }
    }
}


