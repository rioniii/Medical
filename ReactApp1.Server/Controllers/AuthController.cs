using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Services;
using ReactApp1.Server.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using ReactApp1.Server.Data.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server;

[ApiController]
[Route("api/[controller]")]



public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService, ILogger<AuthController> logger, IConfiguration configuration, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userService = userService;
        _logger = logger;
        _configuration = configuration;
        _context = context;
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
                _logger.LogError($"User  with email '{registerUser.Email}' already exists.");
                return StatusCode(StatusCodes.Status403Forbidden,
                    new UserManagerResponse { isSucces = false, Message = "User  already exists!" });
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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new UserManagerResponse { isSucces = false, Message = "This role doesn't exist." });
            }

            // Create the user
            var createResult = await _userManager.CreateAsync(user, registerUser.Password);
            if (!createResult.Succeeded)
            {
                var errorMessage = string.Join(", ", createResult.Errors.Select(e => $"{e.Code} - {e.Description}"));
                _logger.LogError($"Failed to create user: {errorMessage}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new UserManagerResponse { isSucces = false, Message = "User  creation failed" });
            }

            _logger.LogInformation($"User  '{user.Email}' created successfully.");

            // Assign the user to the default role
            var roleResult = await _userManager.AddToRoleAsync(user, defaultRole);
            if (!roleResult.Succeeded)
            {
                var roleErrorMessage = string.Join(", ", roleResult.Errors.Select(e => $"{e.Code} - {e.Description}"));
                _logger.LogError($"Failed to add user to role: {roleErrorMessage}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new UserManagerResponse { isSucces = false, Message = "Failed to add user to role" });
            }

            _logger.LogInformation($"User '{user.Email}' added to role '{defaultRole}' successfully.");
            return StatusCode(StatusCodes.Status200OK,
                new UserManagerResponse { isSucces = true, Message = "User  created successfully!" });

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

        var result = await _userService.LoginUserAsync(loginModel);

        if (!result.isSucces)
        {
            return Unauthorized(new { Message = result.Message });
        }

        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        var (accessToken, refreshToken, expiration, roles) = await _userService.GenerateTokensAsync(user);

        return Ok(new
        {
            token = accessToken,
            refreshToken = refreshToken,
            expiration = expiration,
            roles = roles
        });
    }


    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
    {
        if (string.IsNullOrEmpty(tokenModel.RefreshToken))
        {
            return BadRequest("Refresh token is required.");
        }

        try
        {
            var (accessToken, refreshToken, expiration) = await _userService.RefreshTokenAsync(tokenModel.RefreshToken);

            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshToken,
                expiration = expiration
            });
        }
        catch (SecurityTokenException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
    [HttpPost("Revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        await _userService.RevokeRefreshTokenAsync(userId);

        return NoContent();
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
            return StatusCode(500
            , "Internal server error");
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
    public class TokenModel
    {
        public string RefreshToken { get; set; }
    }

            // Update the user in the database
            await _userManager.UpdateAsync(user);
        }
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



    

