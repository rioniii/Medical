﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

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
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User updatedUser)
        {
            var dbUser = await _context.Users.FindAsync(updatedUser.Id);
            if (dbUser is null)
            {
                return NotFound("User not found!");
            }

            dbUser.Name = updatedUser.Name;
            dbUser.Email = updatedUser.Email;
            dbUser.ConfirmPassword = updatedUser.ConfirmPassword;

            await _context.SaveChangesAsync(); 

            return Ok(dbUser); 
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