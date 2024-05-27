using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientiController : ControllerBase
    {
        public readonly AppDBContext _context;

        public PacientiController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Pacienti>>> GetAllFaturat()
        {
            var pacienti = await _context.Pacienti.ToListAsync();

            return Ok(pacienti);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pacienti>>> GetFaturat(int id)
        {
            var pacienti = await _context.Pacienti.FindAsync(id);
            if (pacienti == null)
                return NotFound("Fatura not found");
            return Ok(pacienti);
        }
        [HttpPost]
        public async Task<ActionResult<List<Pacienti>>> AddFaturat(Pacienti pacientet)
        {
            _context.Pacienti.Add(pacientet);
            await _context.SaveChangesAsync();

            var pacientiList = await _context.Pacienti.ToListAsync();
            return Ok(pacientiList);
        }


        [HttpPatch]
        [Route("UpdateFatura/{id}")]
        public async Task<Pacienti> UpdateFatura(Pacienti objPacienti)
        {
            _context.Entry(objPacienti).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objPacienti;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Pacienti>>> DeletePacienti(int id)
        {
            var dbPacientet = await _context.Pacienti.FindAsync(id);
            if (dbPacientet == null)
                return NotFound("Pacienti not found");

            _context.Pacienti.Remove(dbPacientet);

            await _context.SaveChangesAsync();

            return Ok(await _context.Faturat.ToListAsync()); ;
        }
    }
}

