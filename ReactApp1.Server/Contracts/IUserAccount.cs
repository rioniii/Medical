using ReactApp1.Server.DTOs;
using static ReactApp1.Server.DTOs.ServiceResponses;

namespace ReactApp1.Server.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
