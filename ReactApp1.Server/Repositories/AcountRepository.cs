using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Contracts;
using ReactApp1.Server.Data.Models;
using ReactApp1.Server.DTOs;
using static ReactApp1.Server.DTOs.ServiceResponses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/*
using System.Threading.Tasks;
using System.Collections.Generic;
*/

namespace ReactApp1.Server.Repositories
{
    public class AccountRepository(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
        : IUserAccount
    {


        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty");

            var newUser = new ApplicationUser()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded)
            {
                var errors = string.Join(", ", createUser.Errors.Select(e => e.Description));
                return new GeneralResponse(false, "Error occured.. please try again");
            }
            //Projekti inves dy role user dhe admin

            var isFirstUser = (await userManager.Users.CountAsync()) == 1;
            var roleName = isFirstUser ? "Admin" : "User";


            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return new GeneralResponse(false, $"Error creating role: {errors}");
                }
            }

            var addToRoleResult = await userManager.AddToRoleAsync(newUser, roleName);
            if (!addToRoleResult.Succeeded)
            {
                var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Error adding user to role: {errors}");
            }

            return new GeneralResponse(true, "Account Created");
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser == null)
                return new LoginResponse(false, null, "User not found");

            if (string.IsNullOrEmpty(getUser.Email) || string.IsNullOrEmpty(getUser.UserName))
                return new LoginResponse(false, null, "User data is incomplete");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            if (getUserRole == null || !getUserRole.Any())
                return new LoginResponse(false, null, "User has no assigned roles");

            var userSession = new UserSession(getUser.Id, getUser.UserName, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token, "Login completed");
        }
        public async Task<List<UserDTODetails>> GetUsers()
       
        {
            var users = await userManager.Users.ToListAsync();
            var userDetails = new List<UserDTODetails>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userDetails.Add(new UserDTODetails
                {
                    Id = user.Id,
                    Name = user.Name ?? string.Empty, // Handle potential null values
                    Email = user.Email ?? string.Empty, // Handle potential null values
                    Role = roles.FirstOrDefault() ?? string.Empty // Handle potential null values
                });
            }

            return userDetails;
        }
        public async Task<ServiceResponses.GeneralResponse> UpdateUser(string userId, UserDTODetails userDetailsDTO)
        {
            if (userDetailsDTO == null)
                return new ServiceResponses.GeneralResponse(false, "Model is empty");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new ServiceResponses.GeneralResponse(false, "User not found");

            user.Name = userDetailsDTO.Name;
            user.Email = userDetailsDTO.Email;
            user.UserName = userDetailsDTO.Email; // Ensure the username is updated if the email changes

            var updateUserResult = await userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                var errors = string.Join(", ", updateUserResult.Errors.Select(e => e.Description));
                return new ServiceResponses.GeneralResponse(false, $"Error updating user: {errors}");
            }

            if (!string.IsNullOrEmpty(userDetailsDTO.Role))
            {
                var currentRoles = await userManager.GetRolesAsync(user);                                             
                var removeFromRolesResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeFromRolesResult.Succeeded)
                {
                    var errors = string.Join(", ", removeFromRolesResult.Errors.Select(e => e.Description));
                    return new ServiceResponses.GeneralResponse(false, $"Error removing user from roles: {errors}");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(user, userDetailsDTO.Role);
                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                    return new ServiceResponses.GeneralResponse(false, $"Error adding user to role: {errors}");
                }
            }

            return new ServiceResponses.GeneralResponse(true, "User updated successfully");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}








    //Assign Default Role : Admin to first registrar; rest is user
   /*
    var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }


        public async Task<List<UserDTODetails>> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var userDetails = new List<UserDTODetails>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userDetails.Add(new UserDTODetails
                {
                    id = user.Id,
                    name = user.Name ?? string.Empty,
                    email = user.Email ?? string.Empty, 
                    Role = roles.FirstOrDefault() ?? string.Empty 
                });
            }

            return userDetails;
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser == null)
                return new LoginResponse(false, null, "User not found");

            if (string.IsNullOrEmpty(getUser.Email) || string.IsNullOrEmpty(getUser.UserName))
                return new LoginResponse(false, null, "User data is incomplete");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
   */