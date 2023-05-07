using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class UserUpdateViewComponent : ViewComponent
    {
        private readonly IAppUserService _userService;

        public UserUpdateViewComponent(IAppUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(AppUserUpdateDTO user)
        {
            return View(user);
        }
    }
}
