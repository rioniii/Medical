using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model) {

            if (ModelState.IsValid) 
            { 
                var result = await _userService.RegisterUserAsync(model);

                if (result.isSucces) 
                    return Ok(result); // status code 200

                return BadRequest(result);
            
            }

            return BadRequest("Some properties are not valid"); //Status code 400
        }




    }
}
