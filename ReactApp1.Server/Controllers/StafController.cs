using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StafController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StafController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Staf>>> GetAllStaf()
        {
            var Staf = await _context.Stafi.ToListAsync();
            return Ok(Staf);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Staf>> GetStaf(int id)
        {
            var Staf = await _context.Stafi.FindAsync(id);
            if (Staf is null)
            {
                return NotFound("A Staf with ID: " + id + " doesn't exist!");
            }
            return Ok(Staf);

        }

        [HttpPost]
        public async Task<ActionResult<List<Staf>>> AddStaf(Staf p)
        {

            _context.Stafi.Add(p);
            await _context.SaveChangesAsync();

            return Ok(await _context.Stafi.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Staf>> UpdateStaf(Staf updatedStaf)
        {
            var dbStaf = await _context.Stafi.FindAsync(updatedStaf.Id);
            if (dbStaf is null)
            {
                return NotFound("Staf not found!");
            }

            dbStaf.Emri = updatedStaf.Emri;
            dbStaf.Mbiemri = updatedStaf.Mbiemri;
            dbStaf.Mosha = updatedStaf.Mosha;
            dbStaf.Qyteti = updatedStaf.Qyteti;
            dbStaf.NrTelefonit = updatedStaf.NrTelefonit;
            dbStaf.NiveliEdukimit = updatedStaf.NiveliEdukimit;
            dbStaf.Specializimi = updatedStaf.Specializimi;
            dbStaf.Roli = updatedStaf.Roli;
            dbStaf.Paga = updatedStaf.Paga;
            dbStaf.Image = updatedStaf.Image;



            await _context.SaveChangesAsync();

            return Ok(await _context.Stafi.FindAsync(updatedStaf.Id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Staf>> DeleteStaf(int id)
        {
            var dbStaf = await _context.Stafi.FindAsync(id);
            if (dbStaf is null)
            {
                return NotFound("Staf not found!");
            }

            _context.Stafi.Remove(dbStaf);

            await _context.SaveChangesAsync();
            return Ok(await _context.Stafi.FindAsync(id));

        }
    }
}
