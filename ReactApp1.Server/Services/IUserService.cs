using Microsoft.AspNetCore.Identity;

namespace ReactApp1.Server.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse {
                    Message = "Confirm Password doesnt match the password",
                    isSucces = false
            };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,

            };

            var result = await _userManager.CreateAsync(identityUser,model.Password);
            
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
    }
}
