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
        public readonly AppDBContext _context;

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
        public async Task<ActionResult<List<Sherbimet>>> GetSherbimet(int id)
        {
            var SHERBIMI = await _context.Sherbimi.FindAsync(id);
            if (SHERBIMI == null)
                return NotFound("Role not found");
            return Ok(SHERBIMI);
        }

        [HttpPost]
        public async Task<ActionResult<List<Sherbimet>>> AddSherbimi(Sherbimet user)
        {
            _context.Sherbimi.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Sherbimi.ToListAsync()); ;
        }



        [HttpPatch]
        [Route("UpdateSherbimi/{id}")]
        public async Task<Sherbimet> UpdateSherbimi(Sherbimet inicia)
        {
            _context.Entry(inicia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return inicia;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Sherbimet>>> DeleteSherbimi(int id)
        {
            var dbSherbimet = await _context.Sherbimi.FindAsync(id);
            if (dbSherbimet == null)
                return NotFound("Sherbimi not found");

            _context.Sherbimi.Remove(dbSherbimet);

            await _context.SaveChangesAsync();

            return Ok(await _context.Sherbimi.ToListAsync()); ;
        }
    }
}