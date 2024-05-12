using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.Migrations;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            public readonly AppDBContext _context;

            public UserController(AppDBContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<List<User>>> GetAllUsers()
            {
                var Users = await _context.Users.ToListAsync();
            
            return Ok(Users);
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<List<User>>> GetUser(int id)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound("User not found");
                return Ok(user);
            }

            [HttpPost]
            public async Task<ActionResult<List<User>>> AddUser(User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(await _context.Users.ToListAsync());
            }


            [HttpPatch]
            [Route("UpdateUser/{id}")]
        public async Task<User> UpdateUser(User obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        [HttpDelete]
            public async Task<ActionResult<List<User>>> DeleteUser(int id)
            {
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser == null)
                    return NotFound("User not found");

                _context.Users.Remove(dbUser);

                await _context.SaveChangesAsync();

                return Ok(await _context.Users.ToListAsync()); ;
            }
        }
    }
