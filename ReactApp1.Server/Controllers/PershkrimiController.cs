using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PershkrimiController : ControllerBase
    {
        public readonly AppDBContext _context;

        public PershkrimiController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pershkrimi>>> GetAllPershkrimet()
        {
            var Pershkrimet = await _context.Pershkrimi.ToListAsync();

            return Ok(Pershkrimet);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pershkrimi>>> GetPershkrimi(int id)
        {
            var pershkrimi = await _context.Pershkrimi.FindAsync(id);
            if (pershkrimi == null)
                return NotFound("User not found");
            return Ok(pershkrimi);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pershkrimi>>> AddPershkrimi(Pershkrimi pershkrimi)

        {
            _context.Pershkrimi.Add(pershkrimi);
            await _context.SaveChangesAsync();

            return Ok(await _context.Pershkrimi.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<Pershkrimi>> UpdatePershkrimi(Pershkrimi updatetpershkrimi)
        {
            var dbPershkrimi = await _context.Pershkrimi.FindAsync(updatetpershkrimi.Pershkrimi_Id);
            if (dbPershkrimi is null)
            {
                return NotFound("User not found!");
            }

            dbPershkrimi.Anamneza_Statusi = updatetpershkrimi.Anamneza_Statusi;
            dbPershkrimi.Ekzaminimi = updatetpershkrimi.Ekzaminimi;
            dbPershkrimi.Diagnoza = updatetpershkrimi.Diagnoza;
            dbPershkrimi.Terapia = updatetpershkrimi.Terapia;
            dbPershkrimi.Perfundimi = updatetpershkrimi.Perfundimi;

            await _context.SaveChangesAsync();

            return Ok(dbPershkrimi);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Pershkrimi>>> DeletePershkrimi(int id)
        {
            var dbPershkrimi = await _context.Pershkrimi.FindAsync(id);
            if (dbPershkrimi == null)
                return NotFound("Pershkrimi not found");

            _context.Pershkrimi.Remove(dbPershkrimi);

            await _context.SaveChangesAsync();

            return Ok(await _context.Pershkrimi.ToListAsync()); ;
        }
    }
}

