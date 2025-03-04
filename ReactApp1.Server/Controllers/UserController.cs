﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server;
using ReactApp1.Server.Data.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;

    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterViewModel model)
    {

        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }
    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
    {
        var result = await _userService.GetTokenAsync(model);
        SetRefreshTokenInCookie(result.RefreshToken);
        return Ok(result);
    }


   /* [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PostSecuredData()
    {
        return Ok("This Secured Data is available only for Authenticated Users.");
    }*/

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _userService.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("tokens/{id}")]
    public IActionResult GetRefreshTokens(string id)
    {
        var user = _userService.GetById(id);
        return Ok(user.RefreshTokens);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
    {
        // accept token from request body or cookie
        var token = model.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Token is required" });

        var response = _userService.RevokeToken(token);

        if (!response)
            return NotFound(new { message = "Token not found" });

        return Ok(new { message = "Token revoked" });
    }

    // Method to view all users
    [HttpGet]
    //[Authorize(Roles = "Administrator")] // Only accessible by users with "Administrator" role
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        try
        {
            // Fetch all users from the service
            var users = await _userService.GetAllUsersAsync();

            if (users == null)
            {
                return NotFound("No users found.");
            }

            return Ok(users);  // Return the list of users
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}