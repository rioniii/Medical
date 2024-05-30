using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SherbimetController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SherbimetController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sherbimet>>> GetAllSherbimi()
        {
            var SHERBIMI = await _context.Sherbimi.ToListAsync();

            return Ok(SHERBIMI);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Sherbimet>> GetSherbimet(int id)
        {
            var SHERBIMI = await _context.Sherbimi.FindAsync(id);
            if (SHERBIMI == null)
                return NotFound("Sherbimi not found");
            return Ok(SHERBIMI);
        }

        [HttpPost]
        public async Task<ActionResult<List<Sherbimet>>> AddSherbimi(Sherbimet sherbimi)
        {
            _context.Sherbimi.Add(sherbimi);
            await _context.SaveChangesAsync();

            return Ok(await _context.Sherbimi.ToListAsync());
        }

        [HttpPatch]
        [Route("UpdateSherbimi/{id}")]
        public async Task<ActionResult<Sherbimet>> UpdateSherbimi(int id, Sherbimet sherbimi)
        {
            var dbSherbimi = await _context.Sherbimi.FindAsync(id);
            if (dbSherbimi == null)
                return NotFound("Sherbimi not found");

            _context.Entry(dbSherbimi).CurrentValues.SetValues(sherbimi);
            await _context.SaveChangesAsync();
            return Ok(dbSherbimi);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Sherbimet>>> DeleteSherbimi(int id)
        {
            var dbSherbimi = await _context.Sherbimi.FindAsync(id);
            if (dbSherbimi == null)
                return NotFound("Sherbimi not found");

            _context.Sherbimi.Remove(dbSherbimi);

            await _context.SaveChangesAsync();

            return Ok(await _context.Sherbimi.ToListAsync());
        }
    }
}