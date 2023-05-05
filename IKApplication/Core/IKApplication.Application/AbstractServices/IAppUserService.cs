using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
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
        Task RegisterUserWithCompany(RegisterVM registerVm, string role); 
        Task<List<Sector>> GetSectorsAsync();
    }
}
