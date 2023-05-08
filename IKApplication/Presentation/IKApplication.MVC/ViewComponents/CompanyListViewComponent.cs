using IKApplication.Application.VMs.CompanyVMs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class CompanyListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<CompanyVM> companies)
        {
            return View(companies);
        }
    }
}
