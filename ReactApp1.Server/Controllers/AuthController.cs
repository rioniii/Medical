namespace ReactApp1.Server.Controllers;
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

[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
        }

    // /api/auth/register
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerUser, string role)
    {

        var userExists = await _userManager.FindByEmailAsync(registerUser.Email);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden,
                new UserManagerResponse { isSucces = false, Message = "User already exists!" });
        }

        IdentityUser user = new()
        {
            Email = registerUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerUser.FullName
        };


        if (await _roleManager.RoleExistsAsync(role))
        {

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {


                return StatusCode(StatusCodes.Status500InternalServerError,
                  new UserManagerResponse { isSucces = false, Message = "User Failed to Create!" });
            }
            //Add role to the user...
            await _userManager.AddToRoleAsync(user, role);
            return StatusCode(StatusCodes.Status200OK,
             new UserManagerResponse { isSucces = true, Message = "User created Successfully!" });
        }
        else
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                new UserManagerResponse { isSucces = false, Message = "This Role doesn't Exist." });
        }

    }

    // /api/auth/login
        
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LogInViewModel loginModel)
        {

            var user = await _userManager.FindByNameAsync(loginModel.Email);


            if (user == null && await _userManager.CheckPasswordAsync(user,loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles) 
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    SecurityTokenNoExpirationException = jwtToken.ValidTo

                });
            }
            return Unauthorized();


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


    private JwtSecurityToken GetToken(List<Claim> authClaims) {

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256)
        );

        return token;
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

