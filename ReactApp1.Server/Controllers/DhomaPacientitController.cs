using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhomaPacientitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DhomaPacientitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DhomaPacientit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DhomaPacientitDTO>>> GetDhomaPacientit()
        {
            var dhomaPacienteve = await _context.DhomaPacienteve
                .Include(dp => dp.Pacienti)   // Include the Pacienti related data
                .Include(dp => dp.Dhoma)      // Include the Dhoma related data
                .ToListAsync();

            // Map to DhomaPacientitDTO
            var dhomaPacientitDTOs = dhomaPacienteve.Select(dp => new DhomaPacientitDTO
            {
                Id = dp.Id,
                PacientId = dp.Pacienti.Id,
                DhomaId = dp.Dhoma.Id,
                CheckInDate = dp.CheckInDate,
                CheckOutDate = dp.CheckOutDate
            }).ToList();

            return dhomaPacientitDTOs;
        }

        // GET: api/DhomaPacientit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DhomaPacientitDTO>> GetDhomaPacientit(string id)
        {
            var dhomaPacientit = await _context.DhomaPacienteve
                .Include(dp => dp.Pacienti)
                .Include(dp => dp.Dhoma)
                .FirstOrDefaultAsync(dp => dp.Id.Equals(id));

            if (dhomaPacientit == null)
            {
                return NotFound();
            }

            // Map to DhomaPacientitDTO
            var dhomaPacientitDTO = new DhomaPacientitDTO
            {
                Id = dhomaPacientit.Id,
                PacientId = dhomaPacientit.Pacienti.Id,
                DhomaId = dhomaPacientit.Dhoma.Id,
                CheckInDate = dhomaPacientit.CheckInDate,
                CheckOutDate = dhomaPacientit.CheckOutDate
            };

            return dhomaPacientitDTO;
        }

        // PUT: api/DhomaPacientit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDhomaPacientit(string id, DhomaPacientitDTO dhomaPacientitDTO)
        {
            if (!(id.Equals(dhomaPacientitDTO.Id)))
            {
                return BadRequest();
            }

            // Map DhomaPacientitDTO to DhomaPacientit entity
            var dhomaPacientit = await _context.DhomaPacienteve.FindAsync(id);
            if (dhomaPacientit == null)
            {
                return NotFound();
            }
            dhomaPacientit.Id = dhomaPacientit.Id;
            dhomaPacientit.PacientId = dhomaPacientitDTO.PacientId;
            dhomaPacientit.DhomaId = dhomaPacientitDTO.DhomaId;
            dhomaPacientit.CheckInDate = dhomaPacientitDTO.CheckInDate;
            dhomaPacientit.CheckOutDate = dhomaPacientitDTO.CheckOutDate;

            _context.Entry(dhomaPacientit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DhomaPacientitExists(id))
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

        // POST: api/DhomaPacientit
        [HttpPost]
        public async Task<ActionResult<DhomaPacientitDTO>> PostDhomaPacientit(DhomaPacientitDTO dhomaPacientitDTO)
        {
            // Get the DhomaId as a string
            string dhomaId = dhomaPacientitDTO.DhomaId;

            // Check if the room already has 3 patients
            if (await IsRoomFull(dhomaId))
            {
                return BadRequest("This room already has the maximum allowed number of patients (3).");
            }

            // Create a new DhomaPacientit entity
            var dhomaPacientit = new DhomaPacientit
            {
                Id = Guid.NewGuid().ToString(),  // Ensure a unique ID (e.g., using GUID)
                PacientId = dhomaPacientitDTO.PacientId,
                DhomaId = dhomaId,
                CheckInDate = dhomaPacientitDTO.CheckInDate,
                CheckOutDate = dhomaPacientitDTO.CheckOutDate
            };

            // Add the new record to the database
            _context.DhomaPacienteve.Add(dhomaPacientit);
            await _context.SaveChangesAsync();

            // Set the Id of the DTO to the new entity's Id
            dhomaPacientitDTO.Id = dhomaPacientit.Id;

            return CreatedAtAction("GetDhomaPacientit", new { id = dhomaPacientit.Id }, dhomaPacientitDTO);
        }

        // DELETE: api/DhomaPacientit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDhomaPacientit(string id)
        {
            var dhomaPacientit = await _context.DhomaPacienteve.FindAsync(id);
            if (dhomaPacientit == null)
            {
                return NotFound();
            }

            _context.DhomaPacienteve.Remove(dhomaPacientit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if the room is full (i.e., already has 3 patients)
        private async Task<bool> IsRoomFull(string dhomaId)
        {
            var currentPatientsInRoom = await _context.DhomaPacienteve
                .Where(dp => dp.DhomaId == dhomaId && dp.CheckOutDate == null) // Ensure we are only counting checked-in patients
                .CountAsync();

            return currentPatientsInRoom >= 3;
        }

        // Check if a record exists
        private bool DhomaPacientitExists(string id)
        {
            return _context.DhomaPacienteve.Any(e => e.Id.Equals(id));
        }
    }
}
