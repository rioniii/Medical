//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ReactApp1.Server.Data.Models; // Sigurohuni që kjo është rruga e duhur për modelet tuaja

//namespace ReactApp1.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RecepcionistiController : ControllerBase
//    {
//        private readonly AppDBContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;

//        public RecepcionistiController(AppDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            _context = context;
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        // GET: api/Recepcionisti
//        [HttpGet]
//       /* public async Task<ActionResult<IEnumerable<Recepcionisti>>> GetRecepcionistet()
//        {
//            return await _context.Recepcionistet
//                .Include(r => r.Dhomat)
//                .Include(r => r.User)
//                .ToListAsync();
//        }*/

//        // GET: api/Recepcionisti/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Recepcionisti>> GetRecepcionisti(int id)
//        {
//            var recepcionisti = await _context.Recepcionistet
//                .Include(r => r.Dhomat)
//                .Include(r => r.User)
//                .FirstOrDefaultAsync(r => r.RecepcionistiId == id);

//            if (recepcionisti == null)
//            {
//                return NotFound();
//            }

//            return recepcionisti;
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRecepcionisti(int id, Recepcionisti recepcionisti)
//        {
//            if (id != recepcionisti.RecepcionistiId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(recepcionisti).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!RecepcionistiExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Recepcionisti
//        [HttpPost]
//        public async Task<ActionResult<Recepcionisti>> PostRecepcionisti(Recepcionisti recepcionisti)
//        {
//            _context.Recepcionistet.Add(recepcionisti);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetRecepcionisti", new { id = recepcionisti.RecepcionistiId }, recepcionisti);
//        }

//        // DELETE: api/Recepcionisti/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRecepcionisti(int id)
//        {
//            var recepcionisti = await _context.Recepcionistet.FindAsync(id);
//            if (recepcionisti == null)
//            {
//                return NotFound();
//            }

//            _context.Recepcionistet.Remove(recepcionisti);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool RecepcionistiExists(int id)
//        {
//            return _context.Recepcionistet.Any(e => e.RecepcionistiId == id);
//        }
//    }
//}
