using IKApplication.Application.DTOs.CompanyDTOs;
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
        Task<List<AppUserVM>> GetAllPassiveUsers();
        Task<List<AppUserVM>> GetUsersByCompany(Guid companyId);
        Task<List<AppUserVM>> GetUsersByRole(string role);
        Task<AppUserVM> GetCurrentUserInfo(string userName);
        Task<AppUserVM> GetCurrentUserInfo(Guid id);
        Task<List<RegisterVM>> GetAllRegistrations();
        Task<bool> Login(LoginDTO model);
        Task LogOut();
        Task<IdentityResult> CreateUser(AppUserCreateDTO model, string role);
        Task UpdateUser(AppUserUpdateDTO model);
        Task Delete(Guid id);
        Task<RegisterDTO> CreateRegister();
        Task RegisterUserWithCompany(RegisterDTO register, string role);
        Task AddCompanyManager(AppUserCreateDTO user, CompanyUpdateDTO company);
    }
}
