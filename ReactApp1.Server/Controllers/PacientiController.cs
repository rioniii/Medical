using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReactApp1.Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                _logger.LogError("Failed to get patients: {ExceptionMessage}", ex.ToString());
                return StatusCode(500, "Internal server error - see logs for details");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pacienti>> GetPatient(int id)
        {
            try
            {
                var pacienti = await _context.Pacienti.FindAsync(id);
                if (pacienti == null)
                    return NotFound("Pacienti not found");
                return Ok(pacienti);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get patient: {ExceptionMessage}", ex.ToString());
                return StatusCode(500, "Internal server error - see logs for details");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPatientAndUser(Pacienti pacientet, string userName, string password)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("Failed to add patient and user: {ExceptionMessage}", ex.ToString());
                return StatusCode(500, "Internal server error - see logs for details");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
            catch (Exception ex)
            {
                _logger.LogError("Failed to update patient: {ExceptionMessage}", ex.ToString());
                return StatusCode(500, "Internal server error - see logs for details");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Pacienti>>> DeletePatient(int id)
        {
            try
            {
                var dbPacientet = await _context.Pacienti.FindAsync(id);
                if (dbPacientet == null)
                    return NotFound("Pacienti not found");

                _context.Pacienti.Remove(dbPacientet);
                await _context.SaveChangesAsync();

                return Ok(await _context.Pacienti.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete patient: {ExceptionMessage}", ex.ToString());
                return StatusCode(500, "Internal server error - see logs for details");
            }
        }

        private bool PatientExists(int id)
        {
            return _context.Pacienti.Any(e => e.Patient_Id == id);
        }
    }
}
