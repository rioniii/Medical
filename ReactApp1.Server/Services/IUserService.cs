using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LogInViewModel model);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();


/*        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);*/
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private ApplicationDbContext _context;



        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;

        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm Password doesnt match the password",
                    isSucces = false
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,

            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created succesfully!",
                    isSucces = true,
                };
            }
            return new UserManagerResponse
            {
                Message = "User was not created",
                isSucces = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }

        public async Task<UserManagerResponse> LoginUserAsync(LogInViewModel model)
        {
            if (model == null)
                return new UserManagerResponse
                {
                    Message = "Login model cannot be null.",
                    isSucces = false,
                };

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new UserManagerResponse
                {
                    Message = "Invalid login attempt.",
                    isSucces = false,
                };
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, model.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Shortened expiration time
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                isSucces = true,
                ExpireDate = token.ValidTo
            };
        }
        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

    }
}
