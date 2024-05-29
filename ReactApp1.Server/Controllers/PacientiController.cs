using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<PacientiController> _logger;

        public PacientiController(AppDBContext context, ILogger<PacientiController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
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
                return NotFound("Pacienti not found");
            return Ok(pacienti);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddPatientAndUser(Pacienti pacientet, string userName, string password)
        {
            var user = new ApplicationUser { UserName = userName };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                _context.Pacienti.Add(pacientet);
                await _context.SaveChangesAsync();
                return Ok(await _context.Pacienti.ToListAsync());
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Pacienti patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Patient_Id)
            {
                return BadRequest("Patient ID mismatch.");
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Pacienti.Any(e => e.Patient_Id == id);
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

