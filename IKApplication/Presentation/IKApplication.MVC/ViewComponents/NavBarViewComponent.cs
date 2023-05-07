using IKApplication.Application.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {
        private readonly IAppUserService _userService;

        public NavBarViewComponent(IAppUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await _userService.GetCurrentUserInfo(User.Identity.Name));
        }
    }
}
