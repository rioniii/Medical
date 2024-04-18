using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ContactController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAllContact()
        {
            var Contact = await _context.Contacti.ToListAsync();
            return Ok(Contact);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var Contact = await _context.Contacti.FindAsync(id);
            if (Contact is null)
            {
                return NotFound("A Contact with ID: " + id + " doesn't exist!");
            }
            return Ok(Contact);

        }

        [HttpPost]
        public async Task<ActionResult<List<Contact>>> AddContact(Contact p)
        {

            _context.Contacti.Add(p);
            await _context.SaveChangesAsync();

            return Ok(await _context.Contacti.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> UpdateContact(Contact updatedContact)
        {
            var dbContact = await _context.Contacti.FindAsync(updatedContact.Id);
            if (dbContact is null)
            {
                return NotFound("Contact not found!");
            }

            dbContact.Name = updatedContact.Name;
            dbContact.Email = updatedContact.Email;
            dbContact.Subject = updatedContact.Subject;
            dbContact.Message = updatedContact.Message;



            await _context.SaveChangesAsync();

            return Ok(await _context.Contacti.FindAsync(updatedContact.Id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var dbContact = await _context.Contacti.FindAsync(id);
            if (dbContact is null)
            {
                return NotFound("Contact not found!");
            }

            _context.Contacti.Remove(dbContact);

            await _context.SaveChangesAsync();
            return Ok(await _context.Contacti.FindAsync(id));

        }
    }
}
