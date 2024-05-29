using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfermierController : ControllerBase
    {

        public readonly AppDBContext _context;

        public InfermierController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Infermier>>> GetAllMjekat()
        {
            var Infermieret = await _context.Infermieret.ToListAsync();

            return Ok(Infermieret);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Infermier>>> GetInfermieret(int id)
        {
            var Infermieret = await _context.Infermieret.FindAsync(id);
            if (Infermieret == null)
                return NotFound("Infermieret not found");
            return Ok(Infermieret);
        }

        [HttpPost]
        public async Task<ActionResult<List<Infermier>>> AddInfermieret(Infermier Inf)
        {
            _context.Infermieret.Add(Inf);
            await _context.SaveChangesAsync();

            return Ok(await _context.Infermieret.ToListAsync());
        }


        [HttpPatch]
        [Route("UpdateInfermier/{id}")]
        public async Task<Infermier> UpdateInfermieret(Infermier obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete]
        public async Task<ActionResult<List<Infermier>>> DeleteInfermieret(int id)
        {
            var dbInfermier = await _context.Infermieret.FindAsync(id);
            if (dbInfermier == null)
                return NotFound("Infermieret not found");

            _context.Infermieret.Remove(dbInfermier);

            await _context.SaveChangesAsync();

            return Ok(await _context.Infermieret.ToListAsync()); ;
        }
    }
}



