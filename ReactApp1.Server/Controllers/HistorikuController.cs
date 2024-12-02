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
    public class HistorikuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistorikuController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorikuDTO>>> GetHistoriku()
        {
            var historiks = await _context.Historiks
                .Include(h => h.Mjeku)  
                .Include(h => h.Pacienti) 
                .Select(h => new HistorikuDTO
                {
                    Id = h.Id,
                    MjekuId = h.Mjeku.Id,
                    PacientId = h.Pacienti.Id,
                    Data = h.Data,
                    Anamneza_Statusi = h.Anamneza_Statusi,
                    Ekzaminimi = h.Ekzaminimi,
                    Diagnoza = h.Diagnoza,
                    Terapia = h.Terapia,
                    Perfundimi = h.Perfundimi
                })
                .ToListAsync();

            return Ok(historiks);
        }

        // GET: api/Historiku/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorikuDTO>> GetHistoriku(string id)
        {
            var historiku = await _context.Historiks
                .Include(h => h.Mjeku)
                .Include(h => h.Pacienti)
                .Where(h => h.Id == id)
                .Select(h => new HistorikuDTO
                {
                    Id = h.Id,
                    MjekuId = h.Mjeku.Id,
                    PacientId = h.Pacienti.Id,
                    Data = h.Data,
                    Anamneza_Statusi = h.Anamneza_Statusi,
                    Ekzaminimi = h.Ekzaminimi,
                    Diagnoza = h.Diagnoza,
                    Terapia = h.Terapia,
                    Perfundimi = h.Perfundimi
                })
                .FirstOrDefaultAsync();

            if (historiku == null)
            {
                return NotFound();
            }

            return Ok(historiku);
        }

        // PUT: api/Historiku/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoriku(string id, HistorikuDTO historikuDto)
        {
            if (id != historikuDto.Id)
            {
                return BadRequest();
            }

            var existingHistoriku = await _context.Historiks.FindAsync(id);
            if (existingHistoriku == null)
            {
                return NotFound();
            }
            existingHistoriku.Id = historikuDto.Id;
            existingHistoriku.MjekuId = historikuDto.MjekuId;
            existingHistoriku.PacientId = historikuDto.PacientId;
            existingHistoriku.Data = historikuDto.Data;
            existingHistoriku.Anamneza_Statusi = historikuDto.Anamneza_Statusi;
            existingHistoriku.Ekzaminimi = historikuDto.Ekzaminimi;
            existingHistoriku.Diagnoza = historikuDto.Diagnoza;
            existingHistoriku.Terapia = historikuDto.Terapia;
            existingHistoriku.Perfundimi = historikuDto.Perfundimi;

            _context.Entry(existingHistoriku).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorikuExists(id))
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

        // POST: api/Historiku
        [HttpPost]
        public async Task<ActionResult<Historiku>> PostHistoriku(HistorikuDTO historikuDto)
        {
            var historiku = new Historiku
            {
                Id = historikuDto.Id,
                MjekuId = historikuDto.MjekuId,
                PacientId = historikuDto.PacientId,
                Data = historikuDto.Data,
                Anamneza_Statusi = historikuDto.Anamneza_Statusi,
                Ekzaminimi = historikuDto.Ekzaminimi,
                Diagnoza = historikuDto.Diagnoza,
                Terapia = historikuDto.Terapia,
                Perfundimi = historikuDto.Perfundimi
            };

            _context.Historiks.Add(historiku);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistoriku), new { id = historiku.Id }, historiku);
        }

        // DELETE: api/Historiku/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoriku(string id)
        {
            var historiku = await _context.Historiks.FindAsync(id);
            if (historiku == null)
            {
                return NotFound();
            }

            _context.Historiks.Remove(historiku);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorikuExists(string id)
        {
            return _context.Historiks.Any(e => e.Id == id);
        }
    }
}
