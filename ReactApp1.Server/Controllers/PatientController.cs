using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Options;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
        
    public class PatientController: ControllerBase { 
        private readonly AppDBContext _context;

        public PatientController(AppDBContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAllPatients()
        {
            var Patients = await _context.Patients.ToListAsync();
            return Ok(Patients);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient is null)
            {
                return NotFound("A Patient with ID: " + id + " doesn't exist!");
            }
            return Ok(patient);

        }

        [HttpPost]
        public async Task<ActionResult<List<Patient>>> AddPatient(Patient p)
        {

            _context.Patients.Add(p);
            await _context.SaveChangesAsync();

            return Ok(await _context.Patients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Patient>> UpdatePatient(Patient updatedPatient)
        {
            var dbpatients = await _context.Patients.FindAsync(updatedPatient.Id);
            if(dbpatients is null) {
                return NotFound("Patient not found!");
            }

            dbpatients.Name = updatedPatient.Name;
            dbpatients.Surname = updatedPatient.Surname;
            dbpatients.isRegistered = updatedPatient.isRegistered;
            dbpatients.age = updatedPatient.age;
            
            await _context.SaveChangesAsync();

            return Ok(await _context.Patients.FindAsync(updatedPatient.Id));
        }

        [HttpDelete("{id}")]
        public async  Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var dbpatient = await _context.Patients.FindAsync(id);
            if (dbpatient is null)
            {
                return NotFound("Patient not found!");
            }

            _context.Patients.Remove(dbpatient);

            await _context.SaveChangesAsync();
            return Ok(await _context.Patients.FindAsync(id));
        }
    }
}
