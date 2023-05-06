using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Application.AbstractServices
{
    public interface IAppUserService
    {
        Task<AppUserUpdateDTO> GetByUserName(string userName);
        Task<AppUserUpdateDTO> GetById(Guid id);
        Task<bool> Login(LoginDTO model);
        //Task LogOut();
        Task<IdentityResult> CreateUser(AppUserCreateDTO model, string role);
        Task UpdateUser(AppUserUpdateDTO model);
        Task<AppUserVM> GetCurrentUserInfo(string userName);
    }
}
