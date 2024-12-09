using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TerminiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Termini
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerminiDTO>>> GetTerminet()
        {
            var terminet = await _context.Terminet
                .Include(t => t.Mjeku)
                .Include(t => t.Pacienti)
                .Select(t => new TerminiDTO
                {
                    Id = t.Id,
                    DoktorId = t.DoktorId,
                    PacientId = t.PacientId,
                    DataTerminit = t.DataTerminit,
                    Statusi = t.Statusi
                })
                .ToListAsync();

            return Ok(terminet);
        }

        // GET: api/Termini/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TerminiDTO>> GetTermini(string id)
        {
            var termini = await _context.Terminet
                .Include(t => t.Mjeku)
                .Include(t => t.Pacienti)
                .FirstOrDefaultAsync(t => t.Id.ToString() == id);

            if (termini == null)
            {
                return NotFound();
            }

            var terminiDto = new TerminiDTO
            {
                Id = termini.Id,
                DoktorId = termini.DoktorId,
                PacientId = termini.PacientId,
                DataTerminit = termini.DataTerminit,
                Statusi = termini.Statusi
            };

            return Ok(terminiDto);
        }

        // PUT: api/Termini/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermini(string id, TerminiDTO terminiDto)
        {
            if (id != terminiDto.Id)
            {
                return BadRequest();
            }

            var termini = await _context.Terminet.FindAsync(id);

            if (termini == null)
            {
                return NotFound();
            }
            termini.Id = terminiDto.Id;
            //termini.DoktorId = terminiDto.DoktorId;
            termini.PacientId = terminiDto.PacientId;
            termini.DataTerminit = terminiDto.DataTerminit;
            termini.Statusi = terminiDto.Statusi;

            _context.Entry(termini).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminiExists(id))
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

        // POST: api/Termini
        [HttpPost]
        public async Task<ActionResult<TerminiDTO>> PostTermini(TerminiDTO terminiDto)
        {
            var termini = new Termini
            {
                Id = terminiDto.Id,
                DoktorId = terminiDto.DoktorId,
                PacientId = terminiDto.PacientId,
                DataTerminit = terminiDto.DataTerminit,
                Statusi = terminiDto.Statusi
            };

            _context.Terminet.Add(termini);
            await _context.SaveChangesAsync();

            terminiDto.Id = termini.Id.ToString();

            return CreatedAtAction(nameof(GetTermini), new { id = termini.Id }, terminiDto);
        }

        // DELETE: api/Termini/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermini(string id)
        {
            var termini = await _context.Terminet.FindAsync(id);
            if (termini == null)
            {
                return NotFound();
            }

            _context.Terminet.Remove(termini);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("ShowAppointments-OfDoctor/")]
        public async Task<ActionResult<IEnumerable<TerminiDTO>>> GetTermineByDoctor()
        {
            string doctorId = "1D";
            var termine = await _context.Terminet
                .Include(t => t.Pacienti) // Include the patient entity
                .Where(t => t.DoktorId == doctorId) // Filter by doctor ID
                .Select(t => new TerminiDTO
                {
                    Id = t.Id,
                    PacientId = t.PacientId,
                    Name = t.Pacienti.Name,       
                    Surname = t.Pacienti.Surname,  
                    DoktorId = t.DoktorId,
                    DataTerminit = t.DataTerminit,
                    Statusi = t.Statusi
                })
                .ToListAsync();

            if (termine == null || !termine.Any())
            {
                return NotFound($"No appointments found for doctor with ID {doctorId}.");
            }

            return Ok(termine);
        }



        private bool TerminiExists(string id)
        {
            return _context.Terminet.Any(e => e.Id.Equals(id));
        }
    }
}
