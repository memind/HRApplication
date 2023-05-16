using IKApplication.Application.DTOs.PersonalDTO;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Application.AbstractServices
{
    public interface IAppUserService
    {
        Task<AppUserUpdateDTO> GetByUserName(string userName);
        Task<AppUserUpdateDTO> GetById(Guid id);
        Task<List<AppUserVM>> GetAllUsers();
        Task<List<AppUserVM>> GetUsersByCompany(Guid companyId);
        Task<List<AppUserVM>> GetUsersByRole(string role);
        Task<AppUserVM> GetCurrentUserInfo(string userName);
        Task<List<RegisterVM>> GetAllRegistrations();
        Task<bool> Login(LoginDTO model);
        Task LogOut();
        Task<IdentityResult> CreateUser(AppUserCreateDTO model, string role);
        Task UpdateUser(AppUserUpdateDTO model);
        Task Delete(Guid id);
        Task<RegisterDTO> CreateRegister();
        Task RegisterUserWithCompany(RegisterDTO register, string role);
        Task<IdentityResult> CreatePersonal(PersonalCreateDTO model, string role);
        Task UpdatePersonal(PersonalUpdateDTO model);
    }
}
