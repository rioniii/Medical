using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class RepartController : ControllerBase
        {
            private readonly AppDBContext _context;

            public RepartController(AppDBContext context)
            {
                _context = context;
            }
            [HttpGet]
            public async Task<ActionResult<List<Repart>>> GetAllRepart()
            {
                var Repart = await _context.Reparti.ToListAsync();
                return Ok(Repart);
            }
            [HttpGet("{id}")]
            public async Task<ActionResult<Repart>> GetRepart(int id)
            {
                var Repart = await _context.Reparti.FindAsync(id);
                if (Repart is null)
                {
                    return NotFound("A Repart with ID: " + id + " doesn't exist!");
                }
                return Ok(Repart);

            }

            [HttpPost]
            public async Task<ActionResult<List<Repart>>> AddStaf(Repart p)
            {

                _context.Reparti.Add(p);
                await _context.SaveChangesAsync();

                return Ok(await _context.Reparti.ToListAsync());
            }

            [HttpPut]
            public async Task<ActionResult<Repart>> UpdateRepart(Repart updatedRepart)
            {
                var dbRepart = await _context.Reparti.FindAsync(updatedRepart.Id);
                if (dbRepart is null)
                {
                    return NotFound("Repart not found!");
                }

                dbRepart.Name = updatedRepart.Name;
                dbRepart.kati = updatedRepart.kati;
                dbRepart.NrDhomave = updatedRepart.NrDhomave;
                dbRepart.LlojRepartit = updatedRepart.LlojRepartit;
                dbRepart.NrDoktoreve = updatedRepart.NrDoktoreve;
                dbRepart.NrAssistenteve = updatedRepart.NrAssistenteve;




                await _context.SaveChangesAsync();

                return Ok(await _context.Reparti.FindAsync(updatedRepart.Id));
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<Repart>> DeleteRepart(int id)
            {
                var dbRepart = await _context.Reparti.FindAsync(id);
                if (dbRepart is null)
                {
                    return NotFound("Repart not found!");
                }

                _context.Reparti.Remove(dbRepart);

                await _context.SaveChangesAsync();
                return Ok(await _context.Reparti.FindAsync(id));

            }
        }
    }

}

