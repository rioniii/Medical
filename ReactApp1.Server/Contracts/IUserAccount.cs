﻿using ReactApp1.Server.Data.Models;
using ReactApp1.Server.DTOs;
using static ReactApp1.Server.DTOs.ServiceResponses;

namespace ReactApp1.Server.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<ServiceResponses.LoginResponse> LoginAccount(LoginDTO loginDTO);
        Task<List<UserDTODetails>> GetUsers();

    }
}
