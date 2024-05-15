using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
            public readonly AppDBContext _context;

            public ContactController(AppDBContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<List<Contact>>> GetAllFaturat()
            {
                var contacti = await _context.Contacti.ToListAsync();

                return Ok(contacti);
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<List<Contact>>> GetContacti(int id)
            {
                var contacti = await _context.Contacti.FindAsync(id);
                if (contacti == null)
                    return NotFound("Contacti not found");
                return Ok(contacti);
            }

            [HttpPost]
            public async Task<ActionResult<List<Contact>>> AddContact(Contact contacti)
            {
                _context.Contacti.Add(contacti);
                await _context.SaveChangesAsync();

                return Ok(await _context.Contacti.ToListAsync()); ;
            }



            [HttpPatch]
            [Route("UpdateContacti/{id}")]
            public async Task<Contact> UpdateContact(Contact objContacti)
            {
                _context.Entry(objContacti).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return objContacti;
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<List<Contact>>> DeleteContacti(int id)
            {
                var dbContacti = await _context.Contacti.FindAsync(id);
                if (dbContacti == null)
                    return NotFound("Contacti not found");

                _context.Contacti.Remove(dbContacti);

                await _context.SaveChangesAsync();

                return Ok(await _context.Contacti.ToListAsync()); ;
            }
}
