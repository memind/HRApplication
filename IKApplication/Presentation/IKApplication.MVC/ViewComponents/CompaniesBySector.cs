using IKApplication.Application.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class CompaniesBySector : ViewComponent
    {
        private readonly IDashboardService _dashboardService;

        public CompaniesBySector(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _dashboardService.GetCompanyBySector();
            return View(categories);
        }
    }
}
