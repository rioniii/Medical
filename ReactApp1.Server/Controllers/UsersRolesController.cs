﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data.Models;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersRolesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UsersRolesController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRole()
        {
            var userRoles = await _context.UsersRoless
                                        .Include(ur => ur.User)
                                        .Include(ur => ur.Roles)
                                        .ToListAsync();

            if (userRoles == null || userRoles.Count == 0)
                return NotFound("No user roles found");

            return Ok(userRoles);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRoleById(int id)
        {
            var userRole = await _context.UsersRoless.FindAsync(id);

            if (userRole == null)
                return NotFound("UserRole not found");
            return Ok(userRole);
        }

        [HttpPost]
        public async Task<ActionResult<UserRole>> AddUserRole(int userId, int roleId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound("User not found");
            }

            var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId);
            if (!roleExists)
            {
                return NotFound("Role not found");
            }

            var existingUserRole = await _context.UsersRoless.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (existingUserRole != null)
            {
                return Conflict("User-Role association already exists");
            }

            var user = await _context.Users.FindAsync(userId);
            var role = await _context.Roles.FindAsync(roleId);

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                User = user,
                Roles = role
            };

            _context.UsersRoless.Add(userRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserRole), new { userId = userRole.UserId, roleId = userRole.RoleId }, userRole);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRole>> DeleteUserRole(int id)
        {
            var userRole = await _context.UsersRoless.FindAsync(id);

            if (userRole == null)
                return NotFound("UserRole not found");

            _context.UsersRoless.Remove(userRole);
            await _context.SaveChangesAsync();

            return Ok(userRole);
        }
    }
}