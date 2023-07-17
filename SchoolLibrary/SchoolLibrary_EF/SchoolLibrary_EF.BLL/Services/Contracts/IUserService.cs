﻿using SchoolLibrary_EF.BLL.DTOs.UserDTOs;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Entities.Identity;

namespace SchoolLibrary_EF.BLL.Services.Contracts
{
    public interface IUserService : IGenericService<Guid, GetDTO_User, InsertDTO_User, UpdateDTO_User>
    {
        Task<User?> GetUserWithRefreshTokensAsync(Guid id);
        
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> RegisterAdministratorAsync(RegisterModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> GetRefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
