using IKApplication.Application.VMs.UserVMs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class UserListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<AppUserVM> appUsers)
        {
            return View(appUsers);
        }
    }
}
