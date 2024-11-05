using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ReactApp1.Server.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LogInViewModel model);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<(string accessToken, string refreshToken, DateTime expiration, IList<string> roles)> GenerateTokensAsync(IdentityUser user);
        Task<(string accessToken, string refreshToken, DateTime expiration)> RefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string userId);


        /*        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

                Task<UserManagerResponse> ForgetPasswordAsync(string email);

                Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);*/
    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;



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
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid email or password",
                    isSucces = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid email or password",
                    isSucces = false
                };
            }

            return new UserManagerResponse
            {
                Message = "Login successful",
                isSucces = true
            };
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<(string accessToken, string refreshToken, DateTime expiration, IList<string> roles)> GenerateTokensAsync(IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var expiration = DateTime.UtcNow.AddMinutes(100); // Token expires in 100 minutes

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: expiration,
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = GenerateRefreshToken();

            return (
                accessToken: new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken: refreshToken,
                expiration: expiration,
                roles: userRoles
            );
        }

        public async Task<(string accessToken, string refreshToken, DateTime expiration)> RefreshTokenAsync(string refreshToken)
        {
            var jwtEntry = await _context.JWTs
                .FirstOrDefaultAsync(j => j.RefreshToken == refreshToken);

            if (jwtEntry == null || jwtEntry.ExpiryDate <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }

            var user = await _userManager.FindByIdAsync(jwtEntry.UserId.ToString());
            if (user == null)
            {
                throw new SecurityTokenException("User not found");
            }

            var (accessToken, newRefreshToken, expiration, _) = await GenerateTokensAsync(user);
            return (accessToken, newRefreshToken, expiration);
        }

        public async Task RevokeRefreshTokenAsync(string userId)
        {
            var jwtEntry = await _context.JWTs.FirstOrDefaultAsync(j => j.UserId == int.Parse(userId));
            if (jwtEntry != null)
            {
                jwtEntry.RefreshToken = null;
                jwtEntry.ExpiryDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}