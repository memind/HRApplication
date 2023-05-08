using IKApplication.Application.VMs.UserVMs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class RegistrationListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<RegisterVM> registers)
        {
            return View(registers);
        }
    }
}
