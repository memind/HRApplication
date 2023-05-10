using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.DashboardVMs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class DashboardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DashboardVM dashboard)
        {
            return View(dashboard);
        }
    }
}
