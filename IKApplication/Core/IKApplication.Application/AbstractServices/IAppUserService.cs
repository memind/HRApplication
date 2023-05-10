﻿using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Application.AbstractServices
{
    public interface IAppUserService
    {
        Task<AppUserUpdateDTO> GetByUserName(string userName);
        Task<AppUserUpdateDTO> GetById(Guid id);
        Task<List<AppUserVM>> GetAllUsers();
        Task<AppUserVM> GetCurrentUserInfo(string userName);
        Task<List<RegisterVM>> GetAllRegistrations();
        Task<bool> Login(LoginDTO model);
        Task LogOut();
        Task<IdentityResult> CreateUser(AppUserCreateDTO model, string role);
        Task UpdateUser(AppUserUpdateDTO model);
        Task Delete(Guid id);
        Task<RegisterDTO> CreateRegister();
        Task RegisterUserWithCompany(RegisterDTO register, string role);
    }
}
