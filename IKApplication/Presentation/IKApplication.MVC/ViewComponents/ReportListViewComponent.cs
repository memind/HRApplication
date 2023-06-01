using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.ReportVMs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class ReportListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<ReportVM> reports)
        {
            return View(reports);
        }
    }
}
