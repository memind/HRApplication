using IKApplication.Application.DTOs.SiteManagerDTO;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Application.AbstractServices
{
    public interface IAppUserServices
    {
        Task<SiteManagerUpdateDTO> GetByUserName(string userName);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();
        Task UpdateUser(AppUser model);
        Task<AppUser> GetById(Guid id);
    }
}
