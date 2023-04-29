using IKApplication.Application.DTOs.UserDTOs;
using System.Security.Claims;

namespace IKApplication.Application.AbstractServices
{
    public interface IAppUserService
    {
        Task<bool> Login(LoginDTO model);
    }
}
