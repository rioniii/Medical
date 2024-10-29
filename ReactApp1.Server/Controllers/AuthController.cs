﻿namespace ReactApp1.Server.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NuGet.Common;
namespace ReactApp1.Server.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ReactApp1.Server.Services;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using System.Security.Cryptography;
    using System;
    using System.Linq;
    using ReactApp1.Server.Data.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService, ILogger<AuthController> logger, IConfiguration configuration)
        public AuthController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            ILogger<AuthController> logger,
            IConfiguration configuration,
            User user)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
        }

        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerUser)
        {
            string defaultRole = "User";

            try
            {
                _logger.LogInformation($"Register request received for email: {registerUser.Email}");

                // Check if the user already exists
                if (await _userManager.FindByEmailAsync(registerUser.Email) != null)
                {
                    _logger.LogError($"User with email '{registerUser.Email}' already exists.");
                    return StatusCode(StatusCodes.Status403Forbidden, new UserManagerResponse { isSucces = false, Message = "User already exists!" });
                }

                // Create a new user
                var user = new IdentityUser
                {
                    Email = registerUser.Email,
                    UserName = registerUser.FullName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                // Ensure the default role exists
                if (!await _roleManager.RoleExistsAsync(defaultRole))
                {
                    _logger.LogError($"Role '{defaultRole}' does not exist.");
                    return StatusCode(StatusCodes.Status500InternalServerError, new UserManagerResponse { isSucces = false, Message = "This role doesn't exist." });
                }

                // Create the user
                var createResult = await _userManager.CreateAsync(user, registerUser.Password);
                if (!createResult.Succeeded)
                {
                    var errorMessage = string.Join(", ", createResult.Errors.Select(e => $"{e.Code} - {e.Description}"));
                    _logger.LogError($"Failed to create user: {errorMessage}");
                    return StatusCode(StatusCodes.Status500InternalServerError, new UserManagerResponse { isSucces = false, Message = "User creation failed" });
                }

                _logger.LogInformation($"User '{user.Email}' created successfully.");

                // Assign the user to the default role
                var roleResult = await _userManager.AddToRoleAsync(user, defaultRole);
                if (!roleResult.Succeeded)
                {
                    var roleErrorMessage = string.Join(", ", roleResult.Errors.Select(e => $"{e.Code} - {e.Description}"));
                    _logger.LogError($"Failed to add user to role: {roleErrorMessage}");
                    return StatusCode(StatusCodes.Status500InternalServerError, new UserManagerResponse { isSucces = false, Message = "Failed to add user to role" });
                }

                _logger.LogInformation($"User '{user.Email}' added to role '{defaultRole}' successfully.");
                return StatusCode(StatusCodes.Status200OK, new UserManagerResponse { isSucces = true, Message = "User created successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Message}", ex.Message);
                return StatusCode(500, new UserManagerResponse { isSucces = false, Message = "Internal Server Error." });
            }
        }

        // /api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LogInViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return Unauthorized(new { Message = "Invalid login attempt." });
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken,User);

            // Include user roles in the response
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo,
                roles = userRoles // Include the roles in the response
            });
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users); // Return the list of users
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "Internal server error");
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiresInHours"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                
            };
            return refreshToken;
        }



        [Authorize]
    [HttpPost("ChangeRole")]
    public async Task<IActionResult> ChangeRoleAsync(string userId, string newRole)
    {
        // Ensure the role exists
        if (!await _roleManager.RoleExistsAsync(newRole))
        {
            return BadRequest(new { Message = "The specified role does not exist." });
        }

        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        // Get the user's current roles
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Remove all roles from the user
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error removing roles: {errors}" });
        }

        // Add the user to the new role
        var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
        if (!addRoleResult.Succeeded)
        {
            var errors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error adding new role: {errors}" });
        }

        _logger.LogInformation($"User '{user.Email}' role changed to '{newRole}' successfully.");
        return Ok(new { Message = $"User role changed to {newRole} successfully." });
    }
        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }
    }




    /*        // /api/auth/confirmemail?userid&token
    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return NotFound();

        var result = await _userService.ConfirmEmailAsync(userId, token);

        if (result.IsSuccess)
        {
            return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
        }

        return BadRequest(result);
    }

    // api/auth/forgetpassword
    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> ForgetPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
            return NotFound();

        var result = await _userService.ForgetPasswordAsync(email);

        if (result.IsSuccess)
            return Ok(result); // 200

        return BadRequest(result); // 400
    }

    // api/auth/resetpassword
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.ResetPasswordAsync(model);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        return BadRequest("Some properties are not valid");
    }*/



}

