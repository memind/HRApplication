using IKApplication.Application.AbstractServices;
using IKApplication.Infrastructure.ConcreteServices;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class SideMenuViewComponent : ViewComponent
    {
        private readonly IAppUserService _userService;

        public SideMenuViewComponent(IAppUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View(await _userService.GetCurrentUserInfo(User.Identity.Name));
        }
    }
}
