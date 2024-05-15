using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhomaController : ControllerBase
    {

            public readonly AppDBContext _context;

            public DhomaController(AppDBContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<List<Dhoma>>> GetAllDhomat()
            {
                var Dhomat = await _context.Dhomat.ToListAsync();

                return Ok(Dhomat);
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<List<Dhoma>>> GetDhoma(int id)
            {
                var Dhoma = await _context.Dhomat.FindAsync(id);
                if (Dhoma == null)
                    return NotFound("Dhoma not found");
                return Ok(Dhoma);
            }

            [HttpPost]
            public async Task<ActionResult<List<Dhoma>>> AddDhoma(Dhoma Dhoma)
            {
                _context.Dhomat.Add(Dhoma);
                await _context.SaveChangesAsync();

                return Ok(await _context.Dhomat.ToListAsync());
            }


            [HttpPatch]
            [Route("UpdateDhoma/{id}")]
            public async Task<Dhoma> UpdateDhoma(Dhoma obj)
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return obj;
            }

            [HttpDelete]
            public async Task<ActionResult<List<Dhoma>>> DeleteDhoma(int id)
            {
                var dbDhoma = await _context.Dhomat.FindAsync(id);
                if (dbDhoma == null)
                    return NotFound("Dhoma not found");

                _context.Dhomat.Remove(dbDhoma);

                await _context.SaveChangesAsync();

                return Ok(await _context.Dhomat.ToListAsync()); ;
            }
        }
    }

