using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


 
    public class PacientiController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ILogger<PacientiController> _logger; // Use ILogger<T>

        public PacientiController(AppDBContext context, ILogger<PacientiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pacienti>>> GetAllPatients()
        {
            try
            {
                var patients = await _context.Pacienti.ToListAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get patients: {ExceptionMessage}", ex.ToString()); // Use the logger
                return StatusCode(500, "Internal server error - see logs for details");
            }
        }



        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Pacienti>>> GetPatient(int id)
        {
            var pacienti = await _context.Pacienti.FindAsync(id);
            if (pacienti == null)
                return NotFound("Fatura not found");
            return Ok(pacienti);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<List<Pacienti>>> AddPatient(Pacienti pacientet)
        {
            _context.Pacienti.Add(pacientet);
            await _context.SaveChangesAsync();

            var pacientiList = await _context.Pacienti.ToListAsync();
            return Ok(pacientiList);
        }


        [HttpPatch]
        [AllowAnonymous]
        [Route("UpdateFatura/{id}")]
        public async Task<Pacienti> UpdatePatient(Pacienti objPacienti)
        {
            _context.Entry(objPacienti).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objPacienti;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Pacienti>>> DeletePatient(int id)
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

