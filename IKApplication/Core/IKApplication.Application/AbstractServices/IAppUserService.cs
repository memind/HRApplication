using IKApplication.Application.dtos.UserDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Application.AbstractServices
{
    public interface IAppUserService
    {
        Task<AppUserUpdateDTO> GetByUserName(string userName);
        Task<bool> Login(LoginDTO model);
        Task<IdentityResult> CreateCompanyManagerAsync(AppUserCreateDTO model);
        //Task LogOut();
        Task UpdateUser(AppUserUpdateDTO model);
        Task<AppUserUpdateDTO> GetById(Guid id);
    }
}
