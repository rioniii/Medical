using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturimiController : ControllerBase
    {
        public readonly AppDBContext _context;

        public FaturimiController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Faturimi>>> GetAllFaturat()
        {
            var faturat = await _context.Faturat.ToListAsync();

            return Ok(faturat);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Faturimi>>> GetFaturat(int id)
        {
            var fatura = await _context.Faturat.FindAsync(id);
            if (fatura == null)
                return NotFound("Fatura not found");
            return Ok(fatura);
        }

        [HttpPost]
        public async Task<ActionResult<List<Faturimi>>> AddFaturat(Faturimi faturat)
        {
            _context.Faturat.Add(faturat);
            await _context.SaveChangesAsync();

            return Ok(await _context.Faturat.ToListAsync()); ;
        }



        [HttpPatch]
        [Route("UpdateFatura/{id}")]
        public async Task<Faturimi> UpdateFatura(Faturimi objFaturat)
        {
            _context.Entry(objFaturat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objFaturat;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Faturimi>>> DeleteFaturat(int id)
        {
            var dbFaturat = await _context.Faturat.FindAsync(id);
            if (dbFaturat == null)
                return NotFound("Faturat not found");

            _context.Faturat.Remove(dbFaturat);

            await _context.SaveChangesAsync();

            return Ok(await _context.Faturat.ToListAsync()); ;
        }
    }
}
